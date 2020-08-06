using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Constants;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportTabTotal
    {
        public FundingSummaryReportTabTotal(ICollection<FundingSummaryModel> body)
        {
            IlrST01 = body.Sum(x => x.LearnerAssessmentPlans.ReportValues.Where(rv => rv.Title == FundingSummaryReportConstants.Deliverable_ILR_ST01).Sum(rv => rv.Total));
            EsfST01 = body.Sum(x => x.LearnerAssessmentPlans.ReportValues.Where(rv => rv.Title == FundingSummaryReportConstants.Deliverable_ESF_ST01).Sum(rv => rv.Total));
            EsfCG01 = body.Sum(x => x.CommunityGrants.ReportValues.Where(rv => rv.Title == FundingSummaryReportConstants.Deliverable_ESF_CG01).Sum(rv => rv.Total));
            EsfCG02 = body.Sum(x => x.CommunityGrants.ReportValues.Where(rv => rv.Title == FundingSummaryReportConstants.Deliverable_ESF_CG02).Sum(rv => rv.Total));
            EsfSD01 = body.Sum(x => x.CommunityGrants.ReportValues.Where(rv => rv.Title == FundingSummaryReportConstants.Deliverable_ESF_SD01).Sum(rv => rv.Total));
            EsfSD02 = body.Sum(x => x.CommunityGrants.ReportValues.Where(rv => rv.Title == FundingSummaryReportConstants.Deliverable_ESF_SD02).Sum(rv => rv.Total));
            IlrRQ01StartFunding = body.Sum(x => x.RegulatedLearnings.IlrRQ01StartFunding.Total);
            IlrRQ01AchFunding = body.Sum(x => x.RegulatedLearnings.IlrRQ01AchFunding.Total);
            IlrRQ01Total = body.Sum(x => x.RegulatedLearnings.IlrRQ01SubGroup.Total);
            EsfRQ01AuthClaims = body.Sum(x => x.RegulatedLearnings.EsfRQ01AuthClaims.Total);
            IlrNR01StartFunding = body.Sum(x => x.NonRegulatedLearnings.IlrNR01StartFunding.Total);
            IlrNR01AchFunding = body.Sum(x => x.NonRegulatedLearnings.IlrNR01AchFunding.Total);
            IlrNR01Total = body.Sum(x => x.NonRegulatedLearnings.IlrNR01SubGroup.Total);
            EsfNR01AuthClaims = body.Sum(x => x.NonRegulatedLearnings.EsfNR01AuthClaims.Total);
        }

        public decimal? IlrST01 { get; set; }

        public decimal? EsfST01 { get; set; }

        public decimal? EsfCG01 { get; set; }

        public decimal? EsfCG02 { get; set; }

        public decimal? EsfSD01 { get; set; }

        public decimal? EsfSD02 { get; set; }

        public decimal? IlrRQ01StartFunding { get; set; }

        public decimal? IlrRQ01AchFunding { get; set; }

        public decimal? IlrRQ01Total { get; set; }

        public decimal? EsfRQ01AuthClaims { get; set; }

        public decimal? IlrNR01StartFunding { get; set; }

        public decimal? IlrNR01AchFunding { get; set; }

        public decimal? IlrNR01Total { get; set; }

        public decimal? EsfNR01AuthClaims { get; set; }

        public decimal? LearnerAssessmentPlanTotal => IlrST01 + EsfST01;

        public decimal? CommunityGrantTotal => EsfCG01 + EsfCG02;

        public decimal? SpecificationDefinedTotal => EsfSD01 + EsfSD02;

        public decimal? RegulatedLearningTotal => IlrRQ01Total + EsfRQ01AuthClaims;

        public decimal? NonRegulatedLearningTotal => IlrNR01Total + EsfNR01AuthClaims;

        public decimal? Total => BuildTotal();

        private decimal? BuildTotal()
        {
            return
                LearnerAssessmentPlanTotal +
                CommunityGrantTotal +
                SpecificationDefinedTotal +
                RegulatedLearningTotal +
                NonRegulatedLearningTotal;
        }
    }
}
