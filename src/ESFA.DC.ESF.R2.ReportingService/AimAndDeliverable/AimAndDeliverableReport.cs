using System;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Interface;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;
using ESFA.DC.ESF.R2.ReportingService.Constants;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable
{
    public class AimAndDeliverableReport : AbstractCsvReportService<AimAndDeliverableReportRow, AimAndDeliverableMapper>, IModelReport
    {
        private readonly IAimAndDeliverableModelBuilder _aimAndDeliverableModelBuilder;

        public AimAndDeliverableReport(
            IDateTimeProvider dateTimeProvider,
            ICsvFileService csvFileService,
            IAimAndDeliverableModelBuilder aimAndDeliverableModelBuilder)
            : base(dateTimeProvider, csvFileService, ReportNameConstants.AimsAndDeliverableReport + 2)
        {
            _aimAndDeliverableModelBuilder = aimAndDeliverableModelBuilder;
        }

        public async Task<string> GenerateReport(IEsfJobContext esfJobContext, ISourceFileModel sourceFile, SupplementaryDataWrapper wrapper, CancellationToken cancellationToken)
        {
            var externalFileName = GetExternalFilename(esfJobContext.UkPrn, esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue, ReportNameConstants.CsvExtension);

            // TODO :get data
            var reportModels = _aimAndDeliverableModelBuilder.Build(null, null, null, null, null, null, null);
            await WriteCsv(esfJobContext, externalFileName, reportModels, cancellationToken);

            return externalFileName;
        }
    }
}
