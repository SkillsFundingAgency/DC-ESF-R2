using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.ValidEntities
{
    public partial class LlddandHealthProblem
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Llddcat { get; set; }
        public int? PrimaryLldd { get; set; }
        public long LlddandHealthProblemId { get; set; }

        public virtual Learner Learner { get; set; }
    }
}
