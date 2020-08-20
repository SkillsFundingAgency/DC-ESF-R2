using System.Linq;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class PeriodisedReportValue : IPeriodisedReportValue
    {
        public PeriodisedReportValue(
            string title,
            decimal[] values)
        {
            Title = title;
            MonthlyValues = Enumerable.Range(0, 12).Select(s => values[s]).ToArray();
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
