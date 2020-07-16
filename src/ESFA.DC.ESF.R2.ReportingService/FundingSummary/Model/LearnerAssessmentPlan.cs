namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class LearnerAssessmentPlan
    {
        public GroupHeader GroupHeader { get; set; }

        public PeriodisedReportValue IlrST01 { get; set; }

        public PeriodisedReportValue EsfST01 { get; set; }

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                "Total Learner Assessment and Plan (£)",
                EsfST01.April ?? 0 + IlrST01.April ?? 0,
                EsfST01.May ?? 0 + IlrST01.May ?? 0,
                EsfST01.June ?? 0 + IlrST01.June ?? 0,
                EsfST01.July ?? 0 + IlrST01.July ?? 0,
                EsfST01.August ?? 0 + IlrST01.August ?? 0,
                EsfST01.September ?? 0 + IlrST01.September ?? 0,
                EsfST01.October ?? 0 + IlrST01.October ?? 0,
                EsfST01.November ?? 0 + IlrST01.November ?? 0,
                EsfST01.December ?? 0 + IlrST01.December ?? 0,
                EsfST01.January ?? 0 + IlrST01.January ?? 0,
                EsfST01.February ?? 0 + IlrST01.February ?? 0,
                EsfST01.March ?? 0 + IlrST01.March ?? 0);
        }
    }
}
