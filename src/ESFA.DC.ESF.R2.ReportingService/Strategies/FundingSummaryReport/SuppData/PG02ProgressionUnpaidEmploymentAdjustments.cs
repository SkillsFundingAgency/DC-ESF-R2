using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class PG02ProgressionUnpaidEmploymentAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public PG02ProgressionUnpaidEmploymentAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "PG02";
    }
}