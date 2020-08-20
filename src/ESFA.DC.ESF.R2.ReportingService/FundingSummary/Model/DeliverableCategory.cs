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
                Enumerable.Range(0, 12).Select(s => DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[s])).ToArray());
        }
    }
}
