using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class EsfGlobal
    {
        public EsfGlobal()
        {
            EsfLearners = new HashSet<EsfLearner>();
        }

        public int Ukprn { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<EsfLearner> EsfLearners { get; set; }
    }
}
