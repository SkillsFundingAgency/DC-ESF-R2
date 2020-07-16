namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class RegulatedLearning
    {
        public GroupHeader GroupHeader { get; set; }

        public PeriodisedReportValue IlrRQ01StartFunding { get; set; }

        public PeriodisedReportValue IlrRQ01AchFunding { get; set; }

        public PeriodisedReportValue EsfRQ01AuthClaims { get; set; }

        public PeriodisedReportValue IlrRQ01SubGroup => BuildSubGroup();

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildSubGroup()
        {
            return new PeriodisedReportValue(
                "ILR Total RQ01 Regulated Learning (£)",
                IlrRQ01StartFunding.April ?? 0 + IlrRQ01AchFunding.April ?? 0,
                IlrRQ01StartFunding.May ?? 0 + IlrRQ01AchFunding.May ?? 0,
                IlrRQ01StartFunding.June ?? 0 + IlrRQ01AchFunding.June ?? 0,
                IlrRQ01StartFunding.July ?? 0 + IlrRQ01AchFunding.July ?? 0,
                IlrRQ01StartFunding.August ?? 0 + IlrRQ01AchFunding.August ?? 0,
                IlrRQ01StartFunding.September ?? 0 + IlrRQ01AchFunding.September ?? 0,
                IlrRQ01StartFunding.October ?? 0 + IlrRQ01AchFunding.October ?? 0,
                IlrRQ01StartFunding.November ?? 0 + IlrRQ01AchFunding.November ?? 0,
                IlrRQ01StartFunding.December ?? 0 + IlrRQ01AchFunding.December ?? 0,
                IlrRQ01StartFunding.January ?? 0 + IlrRQ01AchFunding.January ?? 0,
                IlrRQ01StartFunding.February ?? 0 + IlrRQ01AchFunding.February ?? 0,
                IlrRQ01StartFunding.March ?? 0 + IlrRQ01AchFunding.March ?? 0);
        }

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                "Total Regulated Learning (£)",
                IlrRQ01SubGroup.April ?? 0 + EsfRQ01AuthClaims.April ?? 0,
                IlrRQ01SubGroup.May ?? 0 + EsfRQ01AuthClaims.May ?? 0,
                IlrRQ01SubGroup.June ?? 0 + EsfRQ01AuthClaims.June ?? 0,
                IlrRQ01SubGroup.July ?? 0 + EsfRQ01AuthClaims.July ?? 0,
                IlrRQ01SubGroup.August ?? 0 + EsfRQ01AuthClaims.August ?? 0,
                IlrRQ01SubGroup.September ?? 0 + EsfRQ01AuthClaims.September ?? 0,
                IlrRQ01SubGroup.October ?? 0 + EsfRQ01AuthClaims.October ?? 0,
                IlrRQ01SubGroup.November ?? 0 + EsfRQ01AuthClaims.November ?? 0,
                IlrRQ01SubGroup.December ?? 0 + EsfRQ01AuthClaims.December ?? 0,
                IlrRQ01SubGroup.January ?? 0 + EsfRQ01AuthClaims.January ?? 0,
                IlrRQ01SubGroup.February ?? 0 + EsfRQ01AuthClaims.February ?? 0,
                IlrRQ01SubGroup.March ?? 0 + EsfRQ01AuthClaims.March ?? 0);
        }
    }
}
