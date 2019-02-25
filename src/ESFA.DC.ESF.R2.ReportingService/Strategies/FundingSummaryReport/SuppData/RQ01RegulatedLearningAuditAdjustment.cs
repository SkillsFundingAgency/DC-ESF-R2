using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class RQ01RegulatedLearningAuditAdjustment : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public RQ01RegulatedLearningAuditAdjustment(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "RQ01";

        protected override string ReferenceType => "Audit Adjustment";
    }
}