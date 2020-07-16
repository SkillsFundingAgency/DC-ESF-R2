using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportTab
    {
        public string TabName { get; set; }

        public FundingSummaryHeaderModel Header { get; set; }

        public FundingSummaryFooterModel Footer { get; set; }

        public IEnumerable<FundingSummaryModel> Body { get; set; }

        public FundingSummaryReportTabTotal FundingSummaryReportTabTotals => BuildTabTotals();

        private FundingSummaryReportTabTotal BuildTabTotals()
        {
            return new FundingSummaryReportTabTotal(Body);
        }
    }
}
