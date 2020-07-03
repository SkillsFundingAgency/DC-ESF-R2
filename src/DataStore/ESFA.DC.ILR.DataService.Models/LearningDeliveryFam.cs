namespace ESFA.DC.ILR.DataService.Models
{
    public class LearningDeliveryFam
    {
        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string LearnDelFamType { get; set; }

        public string LearnDelFamCode { get; set; }

        public struct LearningDeliveryFamKey
        {
            private readonly string _learnRefNumber;

            private readonly int _aimSeqNumber;

            public LearningDeliveryFamKey(string learnRefNumber, int aimSeqNumber)
            {
                _learnRefNumber = learnRefNumber;
                _aimSeqNumber = aimSeqNumber;
            }
        }
    }
}