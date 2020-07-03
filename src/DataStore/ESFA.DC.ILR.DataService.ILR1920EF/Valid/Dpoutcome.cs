using System;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class Dpoutcome
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string OutType { get; set; }
        public int OutCode { get; set; }
        public DateTime OutStartDate { get; set; }
        public DateTime? OutEndDate { get; set; }
        public DateTime OutCollDate { get; set; }

        public virtual LearnerDestinationandProgression LearnerDestinationandProgression { get; set; }
    }
}
