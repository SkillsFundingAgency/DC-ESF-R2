using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class AecGlobal
    {
        public AecGlobal()
        {
            AecLearners = new HashSet<AecLearner>();
        }

        public int Ukprn { get; set; }
        public string Larsversion { get; set; }
        public string RulebaseVersion { get; set; }
        public string Year { get; set; }

        public virtual ICollection<AecLearner> AecLearners { get; set; }
    }
}
