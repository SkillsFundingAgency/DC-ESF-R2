using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportModel
    {
        public FundingSummaryHeaderModel Header { get; set; }

        public FundingSummaryFooterModel Footer { get; set; }
    }
}
