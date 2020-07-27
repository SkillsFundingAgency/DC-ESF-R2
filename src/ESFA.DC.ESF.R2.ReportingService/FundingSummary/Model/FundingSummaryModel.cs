namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryModel
    {
        public int Year { get; set; }

        public LearnerAssessmentPlan LearnerAssessmentPlans { get; set; }

        public RegulatedLearning RegulatedLearnings { get; set; }

        public NonRegulatedLearning NonRegulatedLearnings { get; set; }

        public Progression Progressions { get; set; }

        public CommunityGrant CommunityGrants { get; set; }

        public SpecificationDefined SpecificationDefineds { get; set; }

        public PeriodisedReportValue MonthlyTotals => BuildMonthlyTotals();

        public PeriodisedReportValue CumulativeMonthlyTotals => BuildCumulativeMonthlyTotals();

        private decimal Sum(
            decimal? value1,
            decimal? value2 = null,
            decimal? value3 = null,
            decimal? value4 = null,
            decimal? value5 = null,
            decimal? value6 = null,
            decimal? value7 = null,
            decimal? value8 = null,
            decimal? value9 = null,
            decimal? value10 = null,
            decimal? value11 = null,
            decimal ? value12 = null)
        {
            return value1 ?? 0 +
                (value2 ?? 0) +
                (value3 ?? 0) +
                (value4 ?? 0) +
                (value5 ?? 0) +
                (value6 ?? 0) +
                (value7 ?? 0) +
                (value8 ?? 0) +
                (value9 ?? 0) +
                (value10 ?? 0) +
                (value11 ?? 0) +
                (value12 ?? 0);
        }

        private PeriodisedReportValue BuildMonthlyTotals()
        {
            return new PeriodisedReportValue(
                "Total (£)",
                Sum(LearnerAssessmentPlans?.Totals.April, CommunityGrants?.Totals.April, SpecificationDefineds?.Totals.April, RegulatedLearnings?.Totals.April, NonRegulatedLearnings?.Totals.April, Progressions?.Totals.April),
                Sum(LearnerAssessmentPlans?.Totals.May, CommunityGrants?.Totals.May, SpecificationDefineds?.Totals.May, RegulatedLearnings?.Totals.May, NonRegulatedLearnings?.Totals.May, Progressions?.Totals.May),
                Sum(LearnerAssessmentPlans?.Totals.June, CommunityGrants?.Totals.June, SpecificationDefineds?.Totals.June, RegulatedLearnings?.Totals.June, NonRegulatedLearnings?.Totals.June, Progressions?.Totals.June),
                Sum(LearnerAssessmentPlans?.Totals.July, CommunityGrants?.Totals.July, SpecificationDefineds?.Totals.July, RegulatedLearnings?.Totals.July, NonRegulatedLearnings?.Totals.July, Progressions?.Totals.July),
                Sum(LearnerAssessmentPlans?.Totals.August, CommunityGrants?.Totals.August, SpecificationDefineds?.Totals.August, RegulatedLearnings?.Totals.August, NonRegulatedLearnings?.Totals.August, Progressions?.Totals.August),
                Sum(LearnerAssessmentPlans?.Totals.September, CommunityGrants?.Totals.September, SpecificationDefineds?.Totals.September, RegulatedLearnings?.Totals.September, NonRegulatedLearnings?.Totals.September, Progressions?.Totals.September),
                Sum(LearnerAssessmentPlans?.Totals.October, CommunityGrants?.Totals.October, SpecificationDefineds?.Totals.October, RegulatedLearnings?.Totals.October, NonRegulatedLearnings?.Totals.October, Progressions?.Totals.October),
                Sum(LearnerAssessmentPlans?.Totals.November, CommunityGrants?.Totals.November, SpecificationDefineds?.Totals.November, RegulatedLearnings?.Totals.November, NonRegulatedLearnings?.Totals.November, Progressions?.Totals.November),
                Sum(LearnerAssessmentPlans?.Totals.December, CommunityGrants?.Totals.December, SpecificationDefineds?.Totals.December, RegulatedLearnings?.Totals.December, NonRegulatedLearnings?.Totals.December, Progressions?.Totals.December),
                Sum(LearnerAssessmentPlans?.Totals.January, CommunityGrants?.Totals.January, SpecificationDefineds?.Totals.January, RegulatedLearnings?.Totals.January, NonRegulatedLearnings?.Totals.January, Progressions?.Totals.January),
                Sum(LearnerAssessmentPlans?.Totals.February, CommunityGrants?.Totals.February, SpecificationDefineds?.Totals.February, RegulatedLearnings?.Totals.February, NonRegulatedLearnings?.Totals.February, Progressions?.Totals.February),
                Sum(LearnerAssessmentPlans?.Totals.March, CommunityGrants?.Totals.March, SpecificationDefineds?.Totals.March, RegulatedLearnings?.Totals.March, NonRegulatedLearnings?.Totals.March, Progressions?.Totals.March));
        }

        private PeriodisedReportValue BuildCumulativeMonthlyTotals()
        {
            return new PeriodisedReportValue(
                "Cumulative (£)",
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