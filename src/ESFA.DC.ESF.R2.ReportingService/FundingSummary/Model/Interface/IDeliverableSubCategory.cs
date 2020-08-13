using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IDeliverableSubCategory
    {
        string SubCategoryTitle { get; }

        bool DisplayTitle { get; }

        ICollection<IPeriodisedReportValue> ReportValues { get; }

        PeriodisedReportValue Totals { get; }
    }
}
