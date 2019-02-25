using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class NR01NonRegulatedActivityAuditAdjustment : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public NR01NonRegulatedActivityAuditAdjustment(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "NR01";

        protected override string ReferenceType => "Audit Adjustment";
    }
}