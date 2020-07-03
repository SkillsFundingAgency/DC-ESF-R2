using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearnerHefinancialSupport
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Fintype { get; set; }
        public int? Finamount { get; set; }
    }
}
