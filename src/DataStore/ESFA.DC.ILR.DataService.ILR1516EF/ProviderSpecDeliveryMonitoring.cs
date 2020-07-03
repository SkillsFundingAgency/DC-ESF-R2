namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class ProviderSpecDeliveryMonitoring
    {
        public int ProviderSpecDeliveryMonitoringId { get; set; }
        public int LearningDeliveryId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? AimSeqNumber { get; set; }
        public string ProvSpecDelMonOccur { get; set; }
        public string ProvSpecDelMon { get; set; }
    }
}
