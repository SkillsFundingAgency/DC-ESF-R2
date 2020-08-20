using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class PeriodisedValue : IPeriodisedValue
    {
        public PeriodisedValue(
            string conRefNumber,
            string deliverableCode,
            string attributeName,
            decimal[] values)
        {
            ConRefNumber = conRefNumber;
            DeliverableCode = deliverableCode;
            AttributeName = attributeName;
            MonthlyValues = Enumerable.Range(0, 12).Select(s => values[s]).ToArray();
        }

        public string ConRefNumber { get; set; }

        public string DeliverableCode { get; set; }

        public string AttributeName { get; set; }

        public decimal[] MonthlyValues { get; set; }
    }
}
