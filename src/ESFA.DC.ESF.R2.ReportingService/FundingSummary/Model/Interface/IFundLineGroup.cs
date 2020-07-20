using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IFundLineGroup : IFundingSummaryReportRow
    {
        IList<IFundLine> FundLines { get; }
    }
}
