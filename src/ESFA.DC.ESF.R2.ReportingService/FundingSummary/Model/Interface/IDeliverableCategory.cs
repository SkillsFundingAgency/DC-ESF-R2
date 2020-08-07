using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IDeliverableCategory
    {
        GroupHeader GroupHeader { get; }

        ICollection<IDeliverableSubCategory> DeliverableSubCategories { get; }

        PeriodisedReportValue Totals { get; }
    }
}
