using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportTab
    {
        public string TabName { get; set; }

        public FundingSummaryHeaderModel Header { get; set; }

        public FundingSummaryFooterModel Footer { get; set; }

        public IDictionary<CollectionYear, IFundingCategory> Body { get; set; }
    }
}
