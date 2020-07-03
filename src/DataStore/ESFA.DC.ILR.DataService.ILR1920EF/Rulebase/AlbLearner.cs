using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Rulebase
{
    public partial class AlbLearner
    {
        public AlbLearner()
        {
            AlbLearnerPeriodisedValues = new HashSet<AlbLearnerPeriodisedValue>();
            AlbLearnerPeriods = new HashSet<AlbLearnerPeriod>();
            AlbLearningDeliveries = new HashSet<AlbLearningDelivery>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }

        public virtual AlbGlobal UkprnNavigation { get; set; }
        public virtual ICollection<AlbLearnerPeriodisedValue> AlbLearnerPeriodisedValues { get; set; }
        public virtual ICollection<AlbLearnerPeriod> AlbLearnerPeriods { get; set; }
        public virtual ICollection<AlbLearningDelivery> AlbLearningDeliveries { get; set; }
    }
}
