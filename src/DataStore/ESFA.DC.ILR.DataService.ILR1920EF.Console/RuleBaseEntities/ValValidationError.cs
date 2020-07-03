using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class ValValidationError
    {
        public int Ukprn { get; set; }
        public long AimSeqNumber { get; set; }
        public string ErrorString { get; set; }
        public string FieldValues { get; set; }
        public string LearnRefNumber { get; set; }
        public string RuleId { get; set; }
    }
}
