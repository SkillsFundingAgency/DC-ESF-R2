using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IFundingSummaryReportTab
    {
        string TabName { get; }

        string Title { get; }

        FundingSummaryReportHeaderModel Header { get; }

        FundingSummaryReportFooterModel Footer { get; }

        ICollection<FundingSummaryReportEarnings> Body { get; }
    }
}
