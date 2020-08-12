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
                ReportValues.Sum(x => x.August ?? 0),
                ReportValues.Sum(x => x.September ?? 0),
                ReportValues.Sum(x => x.October ?? 0),
                ReportValues.Sum(x => x.November ?? 0),
                ReportValues.Sum(x => x.December ?? 0),
                ReportValues.Sum(x => x.January ?? 0),
                ReportValues.Sum(x => x.February ?? 0),
                ReportValues.Sum(x => x.March ?? 0),
                ReportValues.Sum(x => x.April ?? 0),
                ReportValues.Sum(x => x.May ?? 0),
                ReportValues.Sum(x => x.June ?? 0),
                ReportValues.Sum(x => x.July ?? 0));
        }
    }
}
