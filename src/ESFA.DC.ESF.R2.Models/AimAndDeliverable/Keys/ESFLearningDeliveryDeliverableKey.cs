using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models.AimAndDeliverable.Keys
{
    public struct ESFLearningDeliveryDeliverableKey
    {
        public ESFLearningDeliveryDeliverableKey(string learnRefNumber, int aimSequenceNumber, string deliverableCode)
        {
            LearnRefNumber = learnRefNumber;
            AimSequenceNumber = aimSequenceNumber;
            DeliverableCode = deliverableCode;
        }

        public string LearnRefNumber { get; }

        public int AimSequenceNumber { get; }

        public string DeliverableCode { get; }

        public static IEqualityComparer<ESFLearningDeliveryDeliverableKey> Comparer { get; } = new EqualityComparer();

        public override int GetHashCode() =>
            (AimSequenceNumber,
                LearnRefNumber?.ToUpper(),
                DeliverableCode?.ToUpper()).GetHashCode();

        private class EqualityComparer : IEqualityComparer<ESFLearningDeliveryDeliverableKey>
        {
            public bool Equals(ESFLearningDeliveryDeliverableKey x, ESFLearningDeliveryDeliverableKey y)
            {
                return x.GetHashCode() == y.GetHashCode();
            }

            public int GetHashCode(ESFLearningDeliveryDeliverableKey obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
