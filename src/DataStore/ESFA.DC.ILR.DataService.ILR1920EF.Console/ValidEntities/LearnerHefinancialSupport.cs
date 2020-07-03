using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.ValidEntities
{
    public partial class LearnerHefinancialSupport
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Fintype { get; set; }
        public int Finamount { get; set; }

        public virtual LearnerHe LearnerHe { get; set; }
    }
}
