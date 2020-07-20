using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IFundingSubCategory : IFundingSummaryReportRow
    {
        IList<IFundLineGroup> FundLineGroups { get; }

        string FundingSubCategoryTitle { get; }
    }
}
