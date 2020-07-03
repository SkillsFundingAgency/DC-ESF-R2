using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.ValidEntities
{
    public partial class LearningDeliveryFam
    {
        public int LearningDeliveryFamId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string LearnDelFamtype { get; set; }
        public string LearnDelFamcode { get; set; }
        public DateTime? LearnDelFamdateFrom { get; set; }
        public DateTime? LearnDelFamdateTo { get; set; }

        public virtual LearningDelivery LearningDelivery { get; set; }
    }
}
