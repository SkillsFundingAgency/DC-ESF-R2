using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class FS01AdditionalProgrammeCostAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public FS01AdditionalProgrammeCostAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "FS01";
    }
}