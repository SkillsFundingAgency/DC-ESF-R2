namespace ESFA.DC.ILR.DataService.ILR1718EF.Console.Entities
{
    public partial class ValidationErrorByPeriod
    {
        public int Id { get; set; }
        public long? Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int? AimSeqNumber { get; set; }
        public string RuleId { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public int? Period { get; set; }
        public string CollectionPeriodName { get; set; }
        public int CollectionPeriodMonth { get; set; }
        public int CollectionPeriodYear { get; set; }
    }
}
