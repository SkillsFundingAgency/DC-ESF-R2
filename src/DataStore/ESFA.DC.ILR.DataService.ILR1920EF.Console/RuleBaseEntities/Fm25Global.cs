using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class Fm25Global
    {
        public Fm25Global()
        {
            Fm25Learners = new HashSet<Fm25Learner>();
        }

        public int Ukprn { get; set; }
        public string Larsversion { get; set; }
        public string OrgVersion { get; set; }
        public string PostcodeDisadvantageVersion { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<Fm25Learner> Fm25Learners { get; set; }
    }
}
