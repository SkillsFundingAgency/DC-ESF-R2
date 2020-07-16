namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class Progression
    {
        public GroupHeader GroupHeader { get; set; }

        public PeriodisedReportValue IlrPG01 { get; set; }

        public PeriodisedReportValue EsfPG01 { get; set; }

        public PeriodisedReportValue PG01SubGroup => BuildSubGroup("Total Paid Employment Progression (£)", IlrPG01, EsfPG01);

        public PeriodisedReportValue IlrPG03 { get; set; }

        public PeriodisedReportValue EsfPG03 { get; set; }

        public PeriodisedReportValue PG03SubGroup => BuildSubGroup("Total Education Progression (£)", IlrPG03, EsfPG03);

        public PeriodisedReportValue IlrPG04 { get; set; }

        public PeriodisedReportValue EsfPG04 { get; set; }

        public PeriodisedReportValue PG04SubGroup => BuildSubGroup("Total Apprenticeship Progression (£)", IlrPG04, EsfPG04);

        public PeriodisedReportValue IlrPG05 { get; set; }

        public PeriodisedReportValue EsfPG05 { get; set; }

        public PeriodisedReportValue PG05SubGroup => BuildSubGroup("Total Traineeship Progression (£)", IlrPG05, EsfPG05);

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildSubGroup(string title, PeriodisedReportValue pv1, PeriodisedReportValue pv2)
        {
            return new PeriodisedReportValue(
                title,
                pv1.April ?? 0 + pv2.April ?? 0,
                pv1.May ?? 0 + pv2.May ?? 0,
                pv1.June ?? 0 + pv2.June ?? 0,
                pv1.July ?? 0 + pv2.July ?? 0,
                pv1.August ?? 0 + pv2.August ?? 0,
                pv1.September ?? 0 + pv2.September ?? 0,
                pv1.October ?? 0 + pv2.October ?? 0,
                pv1.November ?? 0 + pv2.November ?? 0,
                pv1.December ?? 0 + pv2.December ?? 0,
                pv1.January ?? 0 + pv2.January ?? 0,
                pv1.February ?? 0 + pv2.February ?? 0,
                pv1.March ?? 0 + pv2.March ?? 0);
        }

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                "Total Progression and Sustained Progression (£)",
                PG01SubGroup.April ?? 0 + PG03SubGroup.April ?? 0 + PG04SubGroup.April ?? 0 + PG05SubGroup.April ?? 0,
                PG01SubGroup.May ?? 0 + PG03SubGroup.May ?? 0 + PG04SubGroup.May ?? 0 + PG05SubGroup.May ?? 0,
                PG01SubGroup.June ?? 0 + PG03SubGroup.June ?? 0 + PG04SubGroup.June ?? 0 + PG05SubGroup.June ?? 0,
                PG01SubGroup.July ?? 0 + PG03SubGroup.July ?? 0 + PG04SubGroup.July ?? 0 + PG05SubGroup.July ?? 0,
                PG01SubGroup.August ?? 0 + PG03SubGroup.August ?? 0 + PG04SubGroup.August ?? 0 + PG05SubGroup.August ?? 0,
                PG01SubGroup.April ?? 0 + PG03SubGroup.September ?? 0 + PG04SubGroup.September ?? 0 + PG05SubGroup.September ?? 0,
                PG01SubGroup.September ?? 0 + PG03SubGroup.October ?? 0 + PG04SubGroup.October ?? 0 + PG05SubGroup.October ?? 0,
                PG01SubGroup.October ?? 0 + PG03SubGroup.November ?? 0 + PG04SubGroup.November ?? 0 + PG05SubGroup.November ?? 0,
                PG01SubGroup.December ?? 0 + PG03SubGroup.December ?? 0 + PG04SubGroup.December ?? 0 + PG05SubGroup.December ?? 0,
                PG01SubGroup.January ?? 0 + PG03SubGroup.January ?? 0 + PG04SubGroup.January ?? 0 + PG05SubGroup.January ?? 0,
                PG01SubGroup.February ?? 0 + PG03SubGroup.February ?? 0 + PG04SubGroup.February ?? 0 + PG05SubGroup.February ?? 0,
                PG01SubGroup.March ?? 0 + PG03SubGroup.March ?? 0 + PG04SubGroup.March ?? 0 + PG05SubGroup.March ?? 0);
        }
    }
}
