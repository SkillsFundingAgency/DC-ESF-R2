namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class ProviderSpecLearnerMonitoring
    {
        public int ProviderSpecLearnerMonitoringId { get; set; }
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string ProvSpecLearnMonOccur { get; set; }
        public string ProvSpecLearnMon { get; set; }
    }
}
