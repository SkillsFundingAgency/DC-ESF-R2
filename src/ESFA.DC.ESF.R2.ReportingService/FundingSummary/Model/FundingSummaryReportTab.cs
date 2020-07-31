using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportTab
    {
        public string TabName { get; set; }

        public FundingSummaryReportHeaderModel Header { get; set; }

        public FundingSummaryReportFooterModel Footer { get; set; }

        public ICollection<FundingSummaryModel> Body { get; set; }

        public FundingSummaryReportTabTotal FundingSummaryReportTabTotals => BuildTabTotals();

        private FundingSummaryReportTabTotal BuildTabTotals()
        {
            return new FundingSummaryReportTabTotal(Body);
        }
    }
}
