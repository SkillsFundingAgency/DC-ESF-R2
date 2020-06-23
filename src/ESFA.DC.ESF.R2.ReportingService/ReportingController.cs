using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService
{
    public class ReportingController : IReportingController
    {
        private readonly ILogger _logger;
        private readonly IZipService _zipService;

        private readonly IList<IValidationReport> _validationReports;
        private readonly IList<IModelReport> _esfReports;

        public ReportingController(
            IZipService zipService,
            ILogger logger,
            IList<IValidationReport> validationReports,
            IList<IModelReport> esfReports)
        {
            _zipService = zipService;
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

            var zipFileName = $"{esfJobContext.UkPrn}/{esfJobContext.JobId}{ReportNameConstants.ZipName}";

            await _zipService.CreateZipAsync(zipFileName, reportNames, esfJobContext.BlobContainerName, cancellationToken);
        }
    }
}
