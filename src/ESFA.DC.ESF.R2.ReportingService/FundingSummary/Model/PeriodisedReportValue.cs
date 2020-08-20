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
                august ?? 0,
                september ?? 0,
                october ?? 0,
                november ?? 0,
                december ?? 0,
                january ?? 0,
                february ?? 0,
                march ?? 0,
                april ?? 0,
                may ?? 0,
                june ?? 0,
                july ?? 0
            };
        }

        public string Title { get; set; }

        public decimal[] MonthlyValues { get; set; }

        public decimal Total => BuildTotal();

        private decimal BuildTotal()
        {
            return MonthlyValues.Sum(x => x);
        }
    }
}
