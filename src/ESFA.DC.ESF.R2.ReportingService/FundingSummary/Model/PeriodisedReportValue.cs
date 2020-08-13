using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class PeriodisedReportValue : IPeriodisedReportValue
    {
        public PeriodisedReportValue(
            string title,
            decimal? august,
            decimal? september,
            decimal? october,
            decimal? november,
            decimal? december,
            decimal? january,
            decimal? february,
            decimal? march,
            decimal? april,
            decimal? may,
            decimal? june,
            decimal? july)
        {
            Title = title;
            April = april;
            May = may;
            June = june;
            July = july;
            August = august;
            September = september;
            October = october;
            November = november;
            December = december;
            January = january;
            February = february;
            March = march;
    }

        public string Title { get; set; }

        public decimal? April { get; set; }

        public decimal? May { get; set; }

        public decimal? June { get; set; }

        public decimal? July { get; set; }

        public decimal? August { get; set; }

        public decimal? September { get; set; }

        public decimal? October { get; set; }

        public decimal? November { get; set; }

        public decimal? December { get; set; }

        public decimal? January { get; set; }

        public decimal? February { get; set; }

        public decimal? March { get; set; }

        public decimal? Total => BuildTotal();

        private decimal? BuildTotal()
        {
            return
                April +
                May +
                June +
                July +
                August +
                September +
                October +
                November +
                December +
                January +
                February +
                March;
        }
    }
}
