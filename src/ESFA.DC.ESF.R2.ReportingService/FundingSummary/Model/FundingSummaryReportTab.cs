﻿using System.Collections.Generic;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportTab : IFundingSummaryReportTab
    {
        public string TabName { get; set; }

        public string Title { get; set; }

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
