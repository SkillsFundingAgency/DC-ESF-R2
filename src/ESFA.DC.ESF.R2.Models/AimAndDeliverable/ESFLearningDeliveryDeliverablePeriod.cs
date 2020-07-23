namespace ESFA.DC.ESF.R2.Models.AimAndDeliverable
{
    public class ESFLearningDeliveryDeliverablePeriod
    {
        // Key
        public string LearnRefNumber { get; set; }

        public int AimSequenceNumber { get; set; }

        // Deliverable
        public string DeliverableCode { get; set; }

        public decimal DeliverableUnitCost { get; set; }

        // Deliverable Period
        public int Period { get; set; }

        public long? DeliverableVolume { get; set; }

        public int? ReportingVolume { get; set; }

        public decimal? StartEarnings { get; set; }

        public decimal? AchievementEarnings { get; set; }

        public decimal? AdditionalProgCostEarnings { get; set; }

        public decimal? ProgressionEarnings { get; set; }

        public decimal? TotalEarnings { get; set; }
    }
}
