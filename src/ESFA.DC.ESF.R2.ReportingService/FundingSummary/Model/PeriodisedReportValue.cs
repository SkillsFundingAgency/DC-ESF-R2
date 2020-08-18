using System.Linq;
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
            MonthlyValues = new[]
            {
                august,
                september,
                october,
                november,
                december,
                january,
                february,
                march,
                april,
                may,
                june,
                july
            };
        }

        public string Title { get; set; }

        public decimal?[] MonthlyValues { get; set; }

        public decimal? Total => BuildTotal();

        private decimal? BuildTotal()
        {
            return MonthlyValues?.Sum(x => x ?? 0m);
        }
    }
}
