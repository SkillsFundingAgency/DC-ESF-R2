using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearnerHe1
    {
        public int LearnerHeId { get; set; }
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? Ucasperid { get; set; }
        public long? Ttaccom { get; set; }
    }
}
