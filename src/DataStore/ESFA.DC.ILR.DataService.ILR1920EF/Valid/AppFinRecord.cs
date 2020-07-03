using System;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class AppFinRecord
    {
        public int AppFinRecordId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string AfinType { get; set; }
        public int AfinCode { get; set; }
        public DateTime AfinDate { get; set; }
        public int AfinAmount { get; set; }

        public virtual LearningDelivery LearningDelivery { get; set; }
    }
}
