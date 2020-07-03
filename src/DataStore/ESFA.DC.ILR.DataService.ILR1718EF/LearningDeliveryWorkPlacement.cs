using System;

namespace ESFA.DC.ILR.DataService.ILR1718EF
{
    public partial class LearningDeliveryWorkPlacement
    {
        public int Id { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public DateTime WorkPlaceStartDate { get; set; }
        public DateTime? WorkPlaceEndDate { get; set; }
        public int? WorkPlaceHours { get; set; }
        public int WorkPlaceMode { get; set; }
        public int? WorkPlaceEmpId { get; set; }
    }
}
