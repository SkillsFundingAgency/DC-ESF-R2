using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class NR01NonRegulatedActivityAuthorisedClaims : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public NR01NonRegulatedActivityAuthorisedClaims(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "NR01";

        protected override string CostType => "Authorised Claims";
    }
}