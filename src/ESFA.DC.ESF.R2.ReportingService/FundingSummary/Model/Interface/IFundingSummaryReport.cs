using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IFundingSummaryReport
    {
        IEnumerable<FundingSummaryReportTab> FundingSummaryReportTabs { get; }
    }
}
