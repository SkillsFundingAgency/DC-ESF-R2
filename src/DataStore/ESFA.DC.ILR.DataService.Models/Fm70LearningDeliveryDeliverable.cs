namespace ESFA.DC.ILR.DataService.Models
{
    public class Fm70LearningDeliveryDeliverable
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string DeliverableCode { get; set; }

        public decimal? DeliverableUnitCost { get; set; }
    }
}