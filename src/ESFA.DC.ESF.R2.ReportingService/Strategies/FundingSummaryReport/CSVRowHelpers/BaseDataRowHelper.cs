using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers
{
    public abstract class BaseDataRowHelper
    {
        protected List<FundingSummaryReportYearlyValueModel> InitialiseFundingYears(
            int endYear,
            IEnumerable<SupplementaryDataYearlyModel> esfDataModels)
        {
            var suppData = esfDataModels.SelectMany(m => m.SupplementaryData).ToList();
            var maxMonth = suppData.Where(sd => sd.CalendarYear == endYear)
                .OrderByDescending(sd => sd.CalendarMonth).Select(sd => sd.CalendarMonth).FirstOrDefault();

            var yearlyModels = new List<FundingSummaryReportYearlyValueModel>();
            for (var i = Constants.StartYear; i <= endYear; i++)
            {
                yearlyModels.Add(new FundingSummaryReportYearlyValueModel
                {
                    FundingYear = i,
                    StartMonth = i == Constants.StartYear ? 9 : 12,
                    EndMonth = 12,
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