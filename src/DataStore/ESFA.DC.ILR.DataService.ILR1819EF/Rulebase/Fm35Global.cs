using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class Fm35Global
    {
        public Fm35Global()
        {
            Fm35Learners = new HashSet<Fm35Learner>();
        }

        public int Ukprn { get; set; }
        public string CurFundYr { get; set; }
        public string Larsversion { get; set; }
        public string OrgVersion { get; set; }
        public string PostcodeDisadvantageVersion { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<Fm35Learner> Fm35Learners { get; set; }
    }
}
