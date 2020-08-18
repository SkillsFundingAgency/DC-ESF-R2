using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class DeliverableSubCategory : IDeliverableSubCategory
    {
        public DeliverableSubCategory(string subCategoryTitle, bool displayTitle)
        {
            SubCategoryTitle = subCategoryTitle;
            DisplayTitle = displayTitle;
        }

        public string SubCategoryTitle { get; set; }

        public bool DisplayTitle { get; set; }

        public ICollection<IPeriodisedReportValue> ReportValues { get; set; }

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                SubCategoryTitle,
                ReportValues.Sum(x => x.MonthlyValues[0] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[1] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[2] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[3] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[4] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[5] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[6] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[7] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[8] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[9] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[10] ?? 0),
                ReportValues.Sum(x => x.MonthlyValues[11] ?? 0));
        }
    }
}
