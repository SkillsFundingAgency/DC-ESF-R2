using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr
{
    public class BaseILRDataStrategy
    {
        private const string PeriodPrefix = "Period";

        protected virtual string DeliverableCode { get; set; }

        protected virtual List<string> AttributeNames { get; set; }

        public bool IsMatch(string deliverableCode, List<string> attributeNames = null)
        {
            if (attributeNames == null)
            {
                return deliverableCode.CaseInsensitiveEquals(DeliverableCode);
            }

            var firstNotSecond = attributeNames.Except(AttributeNames).ToList();
            var secondNotFirst = AttributeNames.Except(attributeNames).ToList();
            return deliverableCode.CaseInsensitiveEquals(DeliverableCode) && !firstNotSecond.Any() && !secondNotFirst.Any();
        }

        public void Execute(
            IEnumerable<FM70PeriodisedValuesYearly> ilrData,
            IList<FundingSummaryReportYearlyValueModel> yearlyData)
        {
            var fm70PeriodisedValuesYearlyModels = ilrData.ToList();

            if (!fm70PeriodisedValuesYearlyModels.Any())
            {
                return;
            }

            foreach (var year in fm70PeriodisedValuesYearlyModels)
            {
                var data = year.Fm70PeriodisedValues.Where(d =>
                    d.DeliverableCode.CaseInsensitiveEquals(DeliverableCode) && AttributeNames.Contains(d.AttributeName)).ToList();

                var yearData = yearlyData.FirstOrDefault(yd => yd.FundingYear == year.FundingYear);
                if (yearData == null)
                {
                    continue;
                }

                for (var i = yearData.StartMonth; i <= yearData.EndMonth; i++)
                {
                    yearData.Values.Add(GetPeriodValueSum(data, i));
                }
            }
        }

        private static decimal GetPeriodValueSum(IEnumerable<FM70PeriodisedValues> data, int period)
        {
            return data.Sum(v => (decimal)(v.GetType().GetProperty($"{PeriodPrefix}{period.ToString()}")?.GetValue(v) ?? 0M));
        }
    }
}
