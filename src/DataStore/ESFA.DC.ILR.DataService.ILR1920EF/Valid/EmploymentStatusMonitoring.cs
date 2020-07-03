using System;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class EmploymentStatusMonitoring
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public DateTime DateEmpStatApp { get; set; }
        public string Esmtype { get; set; }
        public int? Esmcode { get; set; }
    }
}
