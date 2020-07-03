using System;

namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class LearnerEmploymentStatus
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int? EmpStat { get; set; }
        public DateTime DateEmpStatApp { get; set; }
        public int? EmpId { get; set; }
        public int? EmpStatMonBsi { get; set; }
        public int? EmpStatMonEii { get; set; }
        public int? EmpStatMonLoe { get; set; }
        public int? EmpStatMonLou { get; set; }
        public int? EmpStatMonPei { get; set; }
        public int? EmpStatMonRon { get; set; }
        public int? EmpStatMonSei { get; set; }
        public int? EmpStatMonSem { get; set; }
    }
}
