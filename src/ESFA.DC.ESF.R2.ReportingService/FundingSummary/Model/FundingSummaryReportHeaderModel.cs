using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportHeaderModel
    {
        public string ProviderName { get; set; }

        public string Ukprn { get; set; }

        public string ContractReferenceNumber { get; set; }

        public string SupplementaryDataFile { get; set; }

        public string LastSupplementaryDataFileUpdate { get; set; }

        public string SecurityClassification { get; set; }

        public IEnumerable<IlrFileDetail> IlrFileDetails { get; set; }
    }
}
