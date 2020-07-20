using System;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryIlrHeaderModel
    {
        public string CollectionYear { get; set; }

        public string IlrFileName { get; set; }

        public DateTime? IlrFileLastUpdated { get; set; }

        public DateTime? IlrFilePrepDate { get; set; }

        public string CollectionClosedMessage { get; set; }
    }
}
