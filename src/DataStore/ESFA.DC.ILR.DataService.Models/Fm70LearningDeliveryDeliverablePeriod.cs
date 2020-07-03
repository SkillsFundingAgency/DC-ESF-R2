namespace ESFA.DC.ILR.DataService.Models
{
    public class Fm70LearningDeliveryDeliverablePeriod
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string DeliverableCode { get; set; }

        public int Period { get; set; }

        public decimal? AchievementEarnings { get; set; }

        public decimal? AdditionalProgCostEarnings { get; set; }

        public long? DeliverableVolume { get; set; }

        public decimal? ProgressionEarnings { get; set; }

        public int? ReportingVolume { get; set; }

        public decimal? StartEarnings { get; set; }
    }
}