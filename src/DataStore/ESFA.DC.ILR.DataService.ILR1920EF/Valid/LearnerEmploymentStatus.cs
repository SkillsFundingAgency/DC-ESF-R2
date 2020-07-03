using System;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
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
