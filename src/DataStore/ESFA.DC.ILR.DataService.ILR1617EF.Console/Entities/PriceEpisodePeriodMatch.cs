namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class PriceEpisodePeriodMatch
    {
        public int Id { get; set; }
        public long Ukprn { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public string LearnRefNumber { get; set; }
        public long AimSeqNumber { get; set; }
        public long CommitmentId { get; set; }
        public long VersionId { get; set; }
        public int Period { get; set; }
        public bool Payable { get; set; }
        public int TransactionType { get; set; }
        public string CollectionPeriodName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
    }
}
