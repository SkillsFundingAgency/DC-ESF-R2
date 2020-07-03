using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.ValidEntities
{
    public partial class AppFinRecord
    {
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
