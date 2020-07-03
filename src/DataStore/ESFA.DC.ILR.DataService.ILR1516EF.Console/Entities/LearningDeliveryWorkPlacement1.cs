using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearningDeliveryWorkPlacement1
    {
        public int LearningDeliveryWorkPlacementId { get; set; }
        public int LearningDeliveryId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? AimSeqNumber { get; set; }
        public DateTime? WorkPlaceStartDate { get; set; }
        public DateTime? WorkPlaceEndDate { get; set; }
        public long? WorkPlaceMode { get; set; }
        public long? WorkPlaceEmpId { get; set; }
    }
}
