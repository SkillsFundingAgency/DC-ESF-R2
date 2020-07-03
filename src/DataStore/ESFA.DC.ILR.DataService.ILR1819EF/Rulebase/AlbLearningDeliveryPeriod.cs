namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class AlbLearningDeliveryPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public int Period { get; set; }
        public decimal? AreaUpliftOnProgPayment { get; set; }
        public decimal? AreaUpliftBalPayment { get; set; }
        public int? Albcode { get; set; }
        public decimal? AlbsupportPayment { get; set; }
        public decimal? LearnDelCarLearnPilotOnProgPayment { get; set; }
        public decimal? LearnDelCarLearnPilotBalPayment { get; set; }

        public virtual AlbLearningDelivery AlbLearningDelivery { get; set; }
    }
}
