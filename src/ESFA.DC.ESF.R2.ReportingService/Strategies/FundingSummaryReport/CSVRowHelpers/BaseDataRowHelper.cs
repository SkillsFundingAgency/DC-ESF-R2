using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers
{
    public abstract class BaseDataRowHelper
    {
        protected List<FundingSummaryReportYearlyValueModel> InitialiseFundingYears(IEnumerable<SupplementaryDataYearlyModel> esfDataModels)
        {
            var suppData = esfDataModels.SelectMany(m => m.SupplementaryData).ToList();
            var maxYear = suppData.Max(m => m.CalendarYear);
            var maxMonth = suppData.Where(sd => sd.CalendarYear == maxYear)
                .OrderByDescending(sd => sd.CalendarMonth).Select(sd => sd.CalendarMonth).FirstOrDefault();

            var yearlyModels = new List<FundingSummaryReportYearlyValueModel>();
            for (var i = 2015; i < 2018; i++)
            {
                yearlyModels.Add(new FundingSummaryReportYearlyValueModel
                {
                    FundingYear = i,
                    StartMonth = i == 2015 ? 6 : 1,
                    EndMonth = 12,
                    Values = new List<decimal>()
                });
            }

            if (maxMonth != null && maxYear != null && ((maxYear == 2018 && maxMonth > 7) || maxYear == 2019))
            {
                yearlyModels.Add(new FundingSummaryReportYearlyValueModel
                {
                    FundingYear = 2018,
                    StartMonth = 1,
                    EndMonth = FundingMonthCalculation(maxMonth.Value),
                    Values = new List<decimal>()
                });
            }

            return yearlyModels;
        }

        private int FundingMonthCalculation(int month)
        {
            var fundingMonth = month + 5;

            if (fundingMonth > 12)
            {
                return fundingMonth - 12;
            }

            return fundingMonth;
        }
    }
}