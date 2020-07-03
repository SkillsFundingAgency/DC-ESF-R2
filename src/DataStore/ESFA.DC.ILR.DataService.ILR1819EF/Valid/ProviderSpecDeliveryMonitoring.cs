namespace ESFA.DC.ILR.DataService.ILR1819EF.Valid
{
    public partial class ProviderSpecDeliveryMonitoring
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string ProvSpecDelMonOccur { get; set; }
        public string ProvSpecDelMon { get; set; }

        public virtual LearningDelivery LearningDelivery { get; set; }
    }
}
