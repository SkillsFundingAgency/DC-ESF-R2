using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IDeliverableCategory
    {
        GroupHeader GroupHeader { get; }

        bool HasSubCategories { get; }

        ICollection<IPeriodisedReportValue> ReportValues { get; }

        PeriodisedReportValue Totals { get; }
    }
}
