using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService
{
    public class ReportingController : IReportingController
    {
        private readonly ILogger _logger;
        private readonly IFileService _persistenceService;

        private readonly IList<IValidationReport> _validationReports;
        private readonly IList<IModelReport> _esfReports;

        public ReportingController(
            IFileService persistenceService,
            ILogger logger,
            IList<IValidationReport> validationReports,
            IList<IModelReport> esfReports)
        {
            _persistenceService = persistenceService;
            _logger = logger;
            _validationReports = validationReports;
            _esfReports = esfReports;
        }

        public async Task FileLevelErrorReport(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            await ProduceReports(jobContextModel, wrapper, sourceFile, cancellationToken, false);
        }

        public async Task ProduceReports(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            await ProduceReports(jobContextModel, wrapper, sourceFile, cancellationToken, true);
        }

        private async Task ProduceReports(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken,
            bool passedFileValidation)
        {
            _logger.LogInfo("ESF Reporting service called");

            var fileName = $"{jobContextModel.UkPrn}/{jobContextModel.JobId}/Reports.zip";
            using (Stream memoryStream = new MemoryStream())
            {
                if (await _persistenceService.ExistsAsync(fileName, jobContextModel.BlobContainerName, cancellationToken))
                {
                    using (var readStream = await
                        _persistenceService.OpenReadStreamAsync(
                            fileName,
                            jobContextModel.BlobContainerName,
                            cancellationToken))
                    {
                        await readStream.CopyToAsync(memoryStream);
                    }
                }

                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Update, true))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (!string.IsNullOrWhiteSpace(sourceFile?.FileName))
                    {
                        foreach (var validationReport in _validationReports)
                        {
                            await validationReport.GenerateReport(jobContextModel, sourceFile, wrapper, archive, cancellationToken);
                        }
                    }

                    if (passedFileValidation)
                    {
                        foreach (var report in _esfReports)
                        {
                            await report.GenerateReport(jobContextModel, sourceFile, archive, cancellationToken);
                        }
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }

                using (var writeStream = await _persistenceService.OpenWriteStreamAsync(
                    fileName,
                    jobContextModel.BlobContainerName,
                    cancellationToken))
                {
                    memoryStream.Position = 0;

                    await memoryStream.CopyToAsync(writeStream);
                }
            }
        }
    }
}
