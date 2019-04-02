using System.Collections.Generic;
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
            cancellationToken.ThrowIfCancellationRequested();

            using (var stream = await
                _persistenceService.OpenWriteStreamAsync(
                    $"{jobContextModel.UkPrn}/{jobContextModel.JobId}/Reports.zip",
                    jobContextModel.BlobContainerName,
                    cancellationToken))
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    foreach (var validationReport in _validationReports)
                    {
                        await validationReport.GenerateReport(jobContextModel, sourceFile, wrapper, archive, cancellationToken);
                    }
                }
            }
        }

        public async Task ProduceReports(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            _logger.LogInfo("ESF Reporting service called");

            using (var stream = await
                _persistenceService.OpenWriteStreamAsync(
                    $"{jobContextModel.UkPrn}/{jobContextModel.JobId}/Reports.zip",
                    jobContextModel.BlobContainerName,
                    cancellationToken))
            {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    foreach (var validationReport in _validationReports)
                    {
                        await validationReport.GenerateReport(jobContextModel, sourceFile, wrapper, archive, cancellationToken);
                    }

                    foreach (var report in _esfReports)
                    {
                        await report.GenerateReport(jobContextModel, sourceFile, archive, cancellationToken);
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
    }
}
