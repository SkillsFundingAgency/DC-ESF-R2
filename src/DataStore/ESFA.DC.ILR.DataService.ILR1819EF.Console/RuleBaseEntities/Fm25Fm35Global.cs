using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class Fm25Fm35Global
    {
        public Fm25Fm35Global()
        {
            Fm25Fm35LearnerPeriodisedValues = new HashSet<Fm25Fm35LearnerPeriodisedValue>();
            Fm25Fm35LearnerPeriods = new HashSet<Fm25Fm35LearnerPeriod>();
        }

        public int Ukprn { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<Fm25Fm35LearnerPeriodisedValue> Fm25Fm35LearnerPeriodisedValues { get; set; }
        public virtual ICollection<Fm25Fm35LearnerPeriod> Fm25Fm35LearnerPeriods { get; set; }
    }
}
