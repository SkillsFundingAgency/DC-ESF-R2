namespace ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport
{
    public sealed class FundingSummaryHeaderModel
    {
        public string ProviderName { get; set; }

        public string[] Ukprn { get; set; }

        public string[] ContractReferenceNumber { get; set; }

        public string[] SupplementaryDataFile { get; set; }

        public string[] LastSupplementaryDataFileUpdate { get; set; }

        public string[] SecurityClassification { get; set; }
    }
}