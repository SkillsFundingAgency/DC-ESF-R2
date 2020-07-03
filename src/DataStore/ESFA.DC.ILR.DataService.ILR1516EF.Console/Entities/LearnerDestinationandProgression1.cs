using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearnerDestinationandProgression1
    {
        public int LearnerDestinationandProgressionId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? Uln { get; set; }
    }
}
