using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService
{
    public class ReportingController : IReportingController
    {
        private readonly ILogger _logger;
        private readonly IStreamableKeyValuePersistenceService _streamableKeyValuePersistenceService;

        private readonly IList<IValidationReport> _validationReports;
        private readonly IList<IModelReport> _esfReports;

        public ReportingController(
            IStreamableKeyValuePersistenceService streamableKeyValuePersistenceService,
            ILogger logger,
            IList<IValidationReport> validationReports,
            IList<IModelReport> esfReports)
        {
            _streamableKeyValuePersistenceService = streamableKeyValuePersistenceService;
            _logger = logger;
            _validationReports = validationReports;
            _esfReports = esfReports;
        }

        public async Task FileLevelErrorReport(
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var validationReport in _validationReports)
                    {
                        await validationReport.GenerateReport(sourceFile, wrapper, archive, cancellationToken);
                    }
                }

                await _streamableKeyValuePersistenceService.SaveAsync($"{sourceFile.UKPRN}_{sourceFile.JobId}_Reports.zip", memoryStream, cancellationToken);
            }
        }

        public async Task ProduceReports(
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            _logger.LogInfo("ESF Reporting service called");

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    foreach (var validationReport in _validationReports)
                    {
                        await validationReport.GenerateReport(sourceFile, wrapper, archive, cancellationToken);
                    }

                    foreach (var report in _esfReports)
                    {
                        await report.GenerateReport(wrapper, sourceFile, archive, cancellationToken);
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                }

                await _streamableKeyValuePersistenceService.SaveAsync($"{sourceFile.UKPRN}_{sourceFile.JobId}_Reports.zip", memoryStream, cancellationToken);
            }
        }
    }
}
