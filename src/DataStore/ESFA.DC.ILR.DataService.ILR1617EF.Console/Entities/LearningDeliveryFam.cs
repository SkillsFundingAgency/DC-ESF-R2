using System;

namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class LearningDeliveryFam
    {
        public int Id { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string LearnDelFamtype { get; set; }
        public string LearnDelFamcode { get; set; }
        public DateTime? LearnDelFamdateFrom { get; set; }
        public DateTime? LearnDelFamdateTo { get; set; }
    }
}
