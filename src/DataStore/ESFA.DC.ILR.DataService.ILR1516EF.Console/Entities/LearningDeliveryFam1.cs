using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearningDeliveryFam1
    {
        public int LearningDeliveryFamId { get; set; }
        public int LearningDeliveryId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? AimSeqNumber { get; set; }
        public string LearnDelFamtype { get; set; }
        public string LearnDelFamcode { get; set; }
        public DateTime? LearnDelFamdateFrom { get; set; }
        public DateTime? LearnDelFamdateTo { get; set; }
    }
}
