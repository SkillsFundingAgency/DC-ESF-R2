namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class SpecificationDefined
    {
        public GroupHeader GroupHeader { get; set; }

        public PeriodisedReportValue EsfSD01 { get; set; }

        public PeriodisedReportValue EsfSD02 { get; set; }

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                "Total Specification Defined (£)",
                EsfSD01.April ?? 0 + EsfSD02.April ?? 0,
                EsfSD01.May ?? 0 + EsfSD02.May ?? 0,
                EsfSD01.June ?? 0 + EsfSD02.June ?? 0,
                EsfSD01.July ?? 0 + EsfSD02.July ?? 0,
                EsfSD01.August ?? 0 + EsfSD02.August ?? 0,
                EsfSD01.September ?? 0 + EsfSD02.September ?? 0,
                EsfSD01.October ?? 0 + EsfSD02.October ?? 0,
                EsfSD01.November ?? 0 + EsfSD02.November ?? 0,
                EsfSD01.December ?? 0 + EsfSD02.December ?? 0,
                EsfSD01.January ?? 0 + EsfSD02.January ?? 0,
                EsfSD01.February ?? 0 + EsfSD02.February ?? 0,
                EsfSD01.March ?? 0 + EsfSD02.March ?? 0);
        }
    }
}
