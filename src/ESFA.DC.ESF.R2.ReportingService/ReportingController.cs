using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService
{
    public class ReportingController : IReportingController
    {
        private readonly ILogger _logger;
        private readonly IFileService _fileService;
        private readonly IZipArchiveService _zipArchiveService;

        private readonly IList<IValidationReport> _validationReports;
        private readonly IList<IModelReport> _esfReports;

        public ReportingController(
            IFileService fileService,
            IZipArchiveService zipArchiveService,
            ILogger logger,
            IList<IValidationReport> validationReports,
            IList<IModelReport> esfReports)
        {
            _fileService = fileService;
            _zipArchiveService = zipArchiveService;
            _logger = logger;
            _validationReports = validationReports;
            _esfReports = esfReports;
        }

        public async Task FileLevelErrorReport(
            IEsfJobContext esfJobContext,
            SupplementaryDataWrapper wrapper,
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            await ProduceReports(esfJobContext, wrapper, sourceFile, cancellationToken, false);
        }

        public async Task ProduceReports(
            IEsfJobContext esfJobContext,
            SupplementaryDataWrapper wrapper,
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            await ProduceReports(esfJobContext, wrapper, sourceFile, cancellationToken, true);
        }

        private async Task ProduceReports(
            IEsfJobContext esfJobContext,
            SupplementaryDataWrapper wrapper,
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken,
            bool passedFileValidation)
        {
            _logger.LogInfo("ESF Reporting service called");

            var reportNames = new List<string>();

            try
            {
                if (!string.IsNullOrWhiteSpace(sourceFile?.FileName))
                {
                    foreach (var validationReport in _validationReports)
                    {
                        reportNames.Add(await validationReport.GenerateReport(esfJobContext, sourceFile, wrapper, cancellationToken));
                    }
                }

                if (passedFileValidation)
                {
                    var reportsToRun = _esfReports.Where(r => esfJobContext.Tasks.Contains(r.TaskName, StringComparer.OrdinalIgnoreCase));
                    foreach (var report in reportsToRun)
                    {
                        reportNames.Add(await report.GenerateReport(esfJobContext, sourceFile, wrapper, cancellationToken));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

            await CreateZipAsync(esfJobContext, reportNames, cancellationToken);
        }

        private async Task CreateZipAsync(IEsfJobContext esfJobContext, IEnumerable<string> reportNames, CancellationToken cancellationToken)
        {
            var zipFileName = $"{esfJobContext.UkPrn}/{esfJobContext.JobId}{ReportNameConstants.ZipName}";

            try
            {
                if (await _fileService.ExistsAsync(zipFileName, esfJobContext.BlobContainerName, cancellationToken))
                {
                    using (var fileStream = await _fileService.OpenReadStreamAsync(zipFileName, esfJobContext.BlobContainerName, cancellationToken))
                    {
                        using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Update, true))
                        {
                            await AddReportsToZip(esfJobContext, archive, reportNames, cancellationToken);
                        }
                    }
                }
                else
                {
                    using (Stream stream = new MemoryStream())
                    {
                        using (var archive = new ZipArchive(stream, ZipArchiveMode.Update, true))
                        {
                            await AddReportsToZip(esfJobContext, archive, reportNames, cancellationToken);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private async Task AddReportsToZip(IEsfJobContext esfJobContext, ZipArchive zipArchive, IEnumerable<string> reportNames, CancellationToken cancellationToken)
        {
            foreach (var report in reportNames)
            {
                using (var fileStream = await _fileService.OpenReadStreamAsync(report, esfJobContext.BlobContainerName, cancellationToken))
                {
                    await _zipArchiveService.AddEntryToZip(zipArchive, fileStream, report, cancellationToken);
                }
            }
        }
    }
}
