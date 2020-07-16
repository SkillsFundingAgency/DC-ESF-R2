namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class NonRegulatedLearning
    {
        public GroupHeader GroupHeader { get; set; }

        public PeriodisedReportValue IlrNR01StartFunding { get; set; }

        public PeriodisedReportValue IlrNR01AchFunding { get; set; }

        public PeriodisedReportValue EsfNR01AuthClaims { get; set; }

        public PeriodisedReportValue IlrNR01SubGroup => BuildSubGroup();

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildSubGroup()
        {
            return new PeriodisedReportValue(
                "ILR Total NR01 Non Regulated Learning (£)",
                IlrNR01StartFunding.April ?? 0 + IlrNR01AchFunding.April ?? 0,
                IlrNR01StartFunding.May ?? 0 + IlrNR01AchFunding.May ?? 0,
                IlrNR01StartFunding.June ?? 0 + IlrNR01AchFunding.June ?? 0,
                IlrNR01StartFunding.July ?? 0 + IlrNR01AchFunding.July ?? 0,
                IlrNR01StartFunding.August ?? 0 + IlrNR01AchFunding.August ?? 0,
                IlrNR01StartFunding.September ?? 0 + IlrNR01AchFunding.September ?? 0,
                IlrNR01StartFunding.October ?? 0 + IlrNR01AchFunding.October ?? 0,
                IlrNR01StartFunding.November ?? 0 + IlrNR01AchFunding.November ?? 0,
                IlrNR01StartFunding.December ?? 0 + IlrNR01AchFunding.December ?? 0,
                IlrNR01StartFunding.January ?? 0 + IlrNR01AchFunding.January ?? 0,
                IlrNR01StartFunding.February ?? 0 + IlrNR01AchFunding.February ?? 0,
                IlrNR01StartFunding.March ?? 0 + IlrNR01AchFunding.March ?? 0);
        }

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                "Total Non Regulated Learning (£)",
                IlrNR01SubGroup.April ?? 0 + EsfNR01AuthClaims.April ?? 0,
                IlrNR01SubGroup.May ?? 0 + EsfNR01AuthClaims.May ?? 0,
                IlrNR01SubGroup.June ?? 0 + EsfNR01AuthClaims.June ?? 0,
                IlrNR01SubGroup.July ?? 0 + EsfNR01AuthClaims.July ?? 0,
                IlrNR01SubGroup.August ?? 0 + EsfNR01AuthClaims.August ?? 0,
                IlrNR01SubGroup.September ?? 0 + EsfNR01AuthClaims.September ?? 0,
                IlrNR01SubGroup.October ?? 0 + EsfNR01AuthClaims.October ?? 0,
                IlrNR01SubGroup.November ?? 0 + EsfNR01AuthClaims.November ?? 0,
                IlrNR01SubGroup.December ?? 0 + EsfNR01AuthClaims.December ?? 0,
                IlrNR01SubGroup.January ?? 0 + EsfNR01AuthClaims.January ?? 0,
                IlrNR01SubGroup.February ?? 0 + EsfNR01AuthClaims.February ?? 0,
                IlrNR01SubGroup.March ?? 0 + EsfNR01AuthClaims.March ?? 0);
        }
    }
}
