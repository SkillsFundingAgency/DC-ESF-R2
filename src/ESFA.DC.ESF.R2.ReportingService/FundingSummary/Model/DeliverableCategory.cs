using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class DeliverableCategory : IDeliverableCategory
    {
        public DeliverableCategory(string categoryTitle, bool hasSubCategories)
        {
            CategoryTitle = categoryTitle;
            HasSubCategories = hasSubCategories;
        }

        public string CategoryTitle { get; set; }

        public bool HasSubCategories { get; set; }

        public GroupHeader GroupHeader { get; set; }

        public ICollection<IPeriodisedReportValue> ReportValues { get; set; }

        public PeriodisedReportValue Totals => BuildTotals();

        private PeriodisedReportValue BuildTotals()
        {
            return new PeriodisedReportValue(
                CategoryTitle,
                ReportValues.Sum(x => x.April ?? 0),
                ReportValues.Sum(x => x.May ?? 0),
                ReportValues.Sum(x => x.June ?? 0),
                ReportValues.Sum(x => x.July ?? 0),
                ReportValues.Sum(x => x.August ?? 0),
                ReportValues.Sum(x => x.September ?? 0),
                ReportValues.Sum(x => x.October ?? 0),
                ReportValues.Sum(x => x.November ?? 0),
                ReportValues.Sum(x => x.December ?? 0),
                ReportValues.Sum(x => x.January ?? 0),
                ReportValues.Sum(x => x.February ?? 0),
                ReportValues.Sum(x => x.March ?? 0));
        }
    }
}
