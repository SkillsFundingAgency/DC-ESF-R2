using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.ValidEntities
{
    public partial class LearnerFam
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string LearnFamtype { get; set; }
        public int LearnFamcode { get; set; }

        public virtual Learner Learner { get; set; }
    }
}
