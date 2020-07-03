namespace ESFA.DC.ILR.DataService.ILR1617EF
{
    public partial class ValidationError1
    {
        public int Id { get; set; }
        public long? Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? AimSeqNumber { get; set; }
        public string RuleId { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public string CollectionPeriodName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
    }
}
