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
                ReportValues.Sum(x => x.MonthlyValues[0]),
                ReportValues.Sum(x => x.MonthlyValues[1]),
                ReportValues.Sum(x => x.MonthlyValues[2]),
                ReportValues.Sum(x => x.MonthlyValues[3]),
                ReportValues.Sum(x => x.MonthlyValues[4]),
                ReportValues.Sum(x => x.MonthlyValues[5]),
                ReportValues.Sum(x => x.MonthlyValues[6]),
                ReportValues.Sum(x => x.MonthlyValues[7]),
                ReportValues.Sum(x => x.MonthlyValues[8]),
                ReportValues.Sum(x => x.MonthlyValues[9]),
                ReportValues.Sum(x => x.MonthlyValues[10]),
                ReportValues.Sum(x => x.MonthlyValues[11]));
        }
    }
}
