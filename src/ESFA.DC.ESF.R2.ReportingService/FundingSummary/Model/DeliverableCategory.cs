using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class DeliverableCategory : IDeliverableCategory
    {
        public DeliverableCategory(string categoryTitle)
        {
            CategoryTitle = categoryTitle;
        }

        public string CategoryTitle { get; set; }

        public GroupHeader GroupHeader { get; set; }

        public ICollection<IDeliverableSubCategory> DeliverableSubCategories { get; set; }

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                CategoryTitle,
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.August ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.September ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.October ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.November ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.December ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.January ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.February ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.March ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.April ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.May ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.June ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.July ?? 0));
        }
    }
}
