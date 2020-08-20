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
                Enumerable.Range(0, 12).Select(s => ReportValues.Sum(x => x.MonthlyValues[s])).ToArray());
        }
    }
}
