using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class ProcessingData
    {
        public long Id { get; set; }
        public int Ukprn { get; set; }
        public long FileDetailsId { get; set; }
        public string ProcessingStep { get; set; }
        public string ExecutionTime { get; set; }
    }
}
