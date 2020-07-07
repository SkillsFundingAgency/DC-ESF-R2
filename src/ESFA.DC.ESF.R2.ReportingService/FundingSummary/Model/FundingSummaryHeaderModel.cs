using System;
using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryHeaderModel
    {
        public string ProviderName { get; set; }

        public int Ukprn { get; set; }

        public string ContractReferenceNumber { get; set; }

        public string SupplementaryDataFile { get; set; }

        public DateTime? LastSupplementaryDataFileUpdate { get; set; }

        public string SecurityClassification { get; set; }

        public IDictionary<CollectionYear, FundingSummaryIlrHeaderModel> IlrHeader { get; set; }
    }
}