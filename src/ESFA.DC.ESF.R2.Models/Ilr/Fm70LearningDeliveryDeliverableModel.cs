namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class Fm70LearningDeliveryDeliverableModel
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string DeliverableCode { get; set; }

        public decimal? DeliverableUnitCost { get; set; }
    }
}