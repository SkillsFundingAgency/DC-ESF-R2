namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class CommunityGrant
    {
        public GroupHeader GroupHeader { get; set; }

        public PeriodisedReportValue EsfCG01 { get; set; }

        public PeriodisedReportValue EsfCG02 { get; set; }

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                "Total Community Grant (£)",
                EsfCG01.April ?? 0 + EsfCG02.April ?? 0,
                EsfCG01.May ?? 0 + EsfCG02.May ?? 0,
                EsfCG01.June ?? 0 + EsfCG02.June ?? 0,
                EsfCG01.July ?? 0 + EsfCG02.July ?? 0,
                EsfCG01.August ?? 0 + EsfCG02.August ?? 0,
                EsfCG01.September ?? 0 + EsfCG02.September ?? 0,
                EsfCG01.October ?? 0 + EsfCG02.October ?? 0,
                EsfCG01.November ?? 0 + EsfCG02.November ?? 0,
                EsfCG01.December ?? 0 + EsfCG02.December ?? 0,
                EsfCG01.January ?? 0 + EsfCG02.January ?? 0,
                EsfCG01.February ?? 0 + EsfCG02.February ?? 0,
                EsfCG01.March ?? 0 + EsfCG02.March ?? 0);
        }
    }
}
