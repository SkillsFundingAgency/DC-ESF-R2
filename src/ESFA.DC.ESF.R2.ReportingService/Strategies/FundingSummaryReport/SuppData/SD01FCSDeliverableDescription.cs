using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class SD01FCSDeliverableDescription : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public SD01FCSDeliverableDescription(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "SD01";
    }
}