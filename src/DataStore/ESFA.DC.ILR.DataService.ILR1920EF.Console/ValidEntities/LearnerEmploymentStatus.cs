using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.ValidEntities
{
    public partial class LearnerEmploymentStatus
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int EmpStat { get; set; }
        public DateTime DateEmpStatApp { get; set; }
        public int? EmpId { get; set; }
        public string AgreeId { get; set; }

        public virtual Learner Learner { get; set; }
    }
}
