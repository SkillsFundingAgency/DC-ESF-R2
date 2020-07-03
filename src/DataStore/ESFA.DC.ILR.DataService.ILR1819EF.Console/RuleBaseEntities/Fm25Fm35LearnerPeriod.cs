using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class Fm25Fm35LearnerPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Period { get; set; }
        public decimal? LnrOnProgPay { get; set; }

        public virtual Fm25Learner Fm25Learner { get; set; }
        public virtual Fm25Fm35Global UkprnNavigation { get; set; }
    }
}
