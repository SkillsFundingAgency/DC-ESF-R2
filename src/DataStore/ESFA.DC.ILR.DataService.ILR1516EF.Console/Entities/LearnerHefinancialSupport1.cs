using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearnerHefinancialSupport1
    {
        public int LearnerHefinancialSupportId { get; set; }
        public int LearnerHeId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? Fintype { get; set; }
        public long? Finamount { get; set; }
    }
}
