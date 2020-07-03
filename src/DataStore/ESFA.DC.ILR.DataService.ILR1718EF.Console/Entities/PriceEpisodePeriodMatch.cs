namespace ESFA.DC.ILR.DataService.ILR1718EF.Console.Entities
{
    public partial class PriceEpisodePeriodMatch
    {
        public int Id { get; set; }
        public long Ukprn { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public string LearnRefNumber { get; set; }
        public int? AimSeqNumber { get; set; }
        public long CommitmentId { get; set; }
        public string VersionId { get; set; }
        public int Period { get; set; }
        public bool Payable { get; set; }
        public int TransactionType { get; set; }
        public string CollectionPeriodName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
        public int? TransactionTypesFlag { get; set; }
    }
}
