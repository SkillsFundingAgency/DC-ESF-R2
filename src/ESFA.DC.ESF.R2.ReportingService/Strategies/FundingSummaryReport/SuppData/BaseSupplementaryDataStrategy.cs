using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class BaseSupplementaryDataStrategy
    {
        private const int EsfStartMonth = 8;
        private const int EsfSecondYearMonthPadding = 5;
        private const int EsfFirstYearMonthPadding = 7;

        private readonly IReferenceDataService _service;

        public BaseSupplementaryDataStrategy(IReferenceDataService service)
        {
            _service = service;
        }

        protected virtual string DeliverableCode { get; set; }

        protected virtual string CostType { get; set; }

        public bool IsMatch(string deliverableCode, string costType = null)
        {
            if (costType != null)
            {
                return deliverableCode == DeliverableCode && costType == CostType;
            }

            return deliverableCode == DeliverableCode;
        }

        public void Execute(
            IEnumerable<SupplementaryDataYearlyModel> data,
            IList<FundingSummaryReportYearlyValueModel> yearlyData)
        {
            foreach (var year in data)
            {
                var yearData = yearlyData.FirstOrDefault(yd => yd.FundingYear == year.FundingYear);
                if (yearData == null)
                {
                    continue;
                }

                for (var i = yearData.StartMonth; i <= yearData.EndMonth; i++)
                {
                    var deliverableData = year.SupplementaryData.Where(supp => (supp.CalendarMonth >= EsfStartMonth ?
                        supp.CalendarMonth - EsfFirstYearMonthPadding == i : supp.CalendarMonth + EsfSecondYearMonthPadding == i)
                                           && supp.DeliverableCode == DeliverableCode);
                    if (CostType != null)
                    {
                        deliverableData =
                            deliverableData.Where(supp => supp.CostType.Equals(CostType, StringComparison.OrdinalIgnoreCase));
                    }

                    if (ESFConstants.UnitCostDeliverableCodes.Contains(DeliverableCode))
                    {
                        yearData.Values.Add(GetUnitCostForUnitTypeDeliverables(deliverableData));
                        continue;
                    }

                    yearData.Values.Add(GetPeriodValueSum(deliverableData));
                }
            }
        }

        private decimal GetPeriodValueSum(IEnumerable<SupplementaryDataModel> data)
        {
            return data?.Sum(v => (decimal)(v.GetType().GetProperty("Value")?.GetValue(v) ?? 0M)) ?? 0;
        }

        private decimal GetUnitCostForUnitTypeDeliverables(IEnumerable<SupplementaryDataModel> data)
        {
            var sample = data.FirstOrDefault();

            if (sample == null)
            {
                return 0M;
            }

            return data.Sum(d => d.CostType?.Equals(ESFConstants.UnitCostDeductionCostType, StringComparison.OrdinalIgnoreCase)
                                 ?? false ? d.Value.GetValueOrDefault() * -1 : d.Value.GetValueOrDefault());
        }
    }
}
