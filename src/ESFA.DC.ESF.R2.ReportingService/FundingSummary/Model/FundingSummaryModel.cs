using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryModel
    {
        public int Year { get; set; }

        public string AcademicYear { get; set; }

        public string ConRefNumber { get; set; }

        public IDeliverableCategory LearnerAssessmentPlans { get; set; }

        public IDeliverableCategory RegulatedLearnings { get; set; }

        public IDeliverableCategory NonRegulatedActivities { get; set; }

        public IDeliverableCategory Progressions { get; set; }

        public IDeliverableCategory CommunityGrants { get; set; }

        public IDeliverableCategory SpecificationDefineds { get; set; }

        public PeriodisedReportValue MonthlyTotals => BuildMonthlyTotals();

        public PeriodisedReportValue CumulativeMonthlyTotals => BuildCumulativeMonthlyTotals();

        public ICollection<IDeliverableCategory> DeliverableCategories => GetCategories();

        public decimal? YearTotal { get; set; }

        public decimal? CumulativeYearTotal { get; set; }

        private ICollection<IDeliverableCategory> GetCategories()
        {
            return new List<IDeliverableCategory>
            {
                LearnerAssessmentPlans,
                RegulatedLearnings,
                NonRegulatedActivities,
                Progressions,
                CommunityGrants,
                SpecificationDefineds
            };
        }

        private decimal Sum(params decimal?[] values) => values.Where(x => x.HasValue).Sum(x => x.Value);

        private PeriodisedReportValue BuildMonthlyTotals()
        {
            return new PeriodisedReportValue(
                string.Concat(ConRefNumber, " Total (£)"),
                Sum(LearnerAssessmentPlans?.Totals.April, CommunityGrants?.Totals.April, SpecificationDefineds?.Totals.April, RegulatedLearnings?.Totals.April, NonRegulatedActivities?.Totals.April, Progressions?.Totals.April),
                Sum(LearnerAssessmentPlans?.Totals.May, CommunityGrants?.Totals.May, SpecificationDefineds?.Totals.May, RegulatedLearnings?.Totals.May, NonRegulatedActivities?.Totals.May, Progressions?.Totals.May),
                Sum(LearnerAssessmentPlans?.Totals.June, CommunityGrants?.Totals.June, SpecificationDefineds?.Totals.June, RegulatedLearnings?.Totals.June, NonRegulatedActivities?.Totals.June, Progressions?.Totals.June),
                Sum(LearnerAssessmentPlans?.Totals.July, CommunityGrants?.Totals.July, SpecificationDefineds?.Totals.July, RegulatedLearnings?.Totals.July, NonRegulatedActivities?.Totals.July, Progressions?.Totals.July),
                Sum(LearnerAssessmentPlans?.Totals.August, CommunityGrants?.Totals.August, SpecificationDefineds?.Totals.August, RegulatedLearnings?.Totals.August, NonRegulatedActivities?.Totals.August, Progressions?.Totals.August),
                Sum(LearnerAssessmentPlans?.Totals.September, CommunityGrants?.Totals.September, SpecificationDefineds?.Totals.September, RegulatedLearnings?.Totals.September, NonRegulatedActivities?.Totals.September, Progressions?.Totals.September),
                Sum(LearnerAssessmentPlans?.Totals.October, CommunityGrants?.Totals.October, SpecificationDefineds?.Totals.October, RegulatedLearnings?.Totals.October, NonRegulatedActivities?.Totals.October, Progressions?.Totals.October),
                Sum(LearnerAssessmentPlans?.Totals.November, CommunityGrants?.Totals.November, SpecificationDefineds?.Totals.November, RegulatedLearnings?.Totals.November, NonRegulatedActivities?.Totals.November, Progressions?.Totals.November),
                Sum(LearnerAssessmentPlans?.Totals.December, CommunityGrants?.Totals.December, SpecificationDefineds?.Totals.December, RegulatedLearnings?.Totals.December, NonRegulatedActivities?.Totals.December, Progressions?.Totals.December),
                Sum(LearnerAssessmentPlans?.Totals.January, CommunityGrants?.Totals.January, SpecificationDefineds?.Totals.January, RegulatedLearnings?.Totals.January, NonRegulatedActivities?.Totals.January, Progressions?.Totals.January),
                Sum(LearnerAssessmentPlans?.Totals.February, CommunityGrants?.Totals.February, SpecificationDefineds?.Totals.February, RegulatedLearnings?.Totals.February, NonRegulatedActivities?.Totals.February, Progressions?.Totals.February),
                Sum(LearnerAssessmentPlans?.Totals.March, CommunityGrants?.Totals.March, SpecificationDefineds?.Totals.March, RegulatedLearnings?.Totals.March, NonRegulatedActivities?.Totals.March, Progressions?.Totals.March));
        }

        private PeriodisedReportValue BuildCumulativeMonthlyTotals()
        {
            return new PeriodisedReportValue(
                string.Concat(ConRefNumber, " Cumulative (£)"),
                Sum(MonthlyTotals.April),
                Sum(MonthlyTotals.April, MonthlyTotals.May),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September, MonthlyTotals.October),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September, MonthlyTotals.October, MonthlyTotals.November),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September, MonthlyTotals.October, MonthlyTotals.November, MonthlyTotals.December),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September, MonthlyTotals.October, MonthlyTotals.November, MonthlyTotals.December, MonthlyTotals.January),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September, MonthlyTotals.October, MonthlyTotals.November, MonthlyTotals.December, MonthlyTotals.January, MonthlyTotals.February),
                Sum(MonthlyTotals.April, MonthlyTotals.May, MonthlyTotals.June, MonthlyTotals.July, MonthlyTotals.August, MonthlyTotals.September, MonthlyTotals.October, MonthlyTotals.November, MonthlyTotals.December, MonthlyTotals.January, MonthlyTotals.February, MonthlyTotals.March));
        }
    }
}