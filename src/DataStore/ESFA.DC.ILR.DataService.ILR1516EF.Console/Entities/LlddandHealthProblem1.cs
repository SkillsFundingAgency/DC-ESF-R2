using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LlddandHealthProblem1
    {
        public int LlddandHealthProblemId { get; set; }
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? Llddcat { get; set; }
        public long? PrimaryLldd { get; set; }
    }
}
