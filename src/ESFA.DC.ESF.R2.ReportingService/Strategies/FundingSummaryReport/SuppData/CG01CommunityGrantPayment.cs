using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class CG01CommunityGrantPayment : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public CG01CommunityGrantPayment(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "CG01";
    }
}