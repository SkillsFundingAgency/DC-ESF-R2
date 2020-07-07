using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportModel : IFundingSummaryReport
    {
       public IEnumerable<FundingSummaryReportTab> FundingSummaryReportTabs { get; set; }
    }
}
