using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class ValLearner
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }

        public virtual ValGlobal UkprnNavigation { get; set; }
    }
}
