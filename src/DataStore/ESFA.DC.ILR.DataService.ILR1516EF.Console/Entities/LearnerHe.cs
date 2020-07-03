using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearnerHe
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? Ucasperid { get; set; }
        public int? Ttaccom { get; set; }
    }
}
