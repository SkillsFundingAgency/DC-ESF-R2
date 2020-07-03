namespace ESFA.DC.ILR.DataService.ILR1718EF.Console.Entities
{
    public partial class EsfLearningDeliveryDeliverable
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string DeliverableCode { get; set; }
        public decimal? DeliverableUnitCost { get; set; }
    }
}
