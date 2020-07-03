using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class DvGlobal
    {
        public DvGlobal()
        {
            DvLearners = new HashSet<DvLearner>();
        }

        public int Ukprn { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<DvLearner> DvLearners { get; set; }
    }
}
