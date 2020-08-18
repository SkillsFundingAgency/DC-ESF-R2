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
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[0] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[1] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[2] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[3] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[4] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[5] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[6] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[7] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[8] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[9] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[10] ?? 0),
                DeliverableSubCategories.SelectMany(x => x.ReportValues).Sum(x => x.MonthlyValues[11] ?? 0));
        }
    }
}
