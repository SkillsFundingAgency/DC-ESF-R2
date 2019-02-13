using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport
{
    public class FundingSummaryReportYearlyValueModel
    {
        public FundingSummaryReportYearlyValueModel()
        {
            Values = new List<decimal>();
        }

        public int FundingYear { get; set; }

        public int StartMonth { get; set; }

        public int EndMonth { get; set; }

        public List<decimal> Values { get; set; }
    }
}