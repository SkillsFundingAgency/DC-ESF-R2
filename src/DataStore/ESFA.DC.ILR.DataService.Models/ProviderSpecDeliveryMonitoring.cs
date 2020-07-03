namespace ESFA.DC.ILR.DataService.Models
{
    public class ProviderSpecDeliveryMonitoring
    {
        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string ProvSpecDelMonOccur { get; set; }

        public string ProvSpecDelMon { get; set; }

        public struct ProviderSpecDeliveryMonitoringKey
        {
            private readonly string _learnRefNumber;

            private readonly int _aimSeqNumber;

            public ProviderSpecDeliveryMonitoringKey(string learnRefNumber, int aimSeqNumber)
            {
                _learnRefNumber = learnRefNumber;
                _aimSeqNumber = aimSeqNumber;
            }
        }
    }
}