using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class ST01LearnerAssessmentAndPlanAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public ST01LearnerAssessmentAndPlanAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "ST01";
    }
}