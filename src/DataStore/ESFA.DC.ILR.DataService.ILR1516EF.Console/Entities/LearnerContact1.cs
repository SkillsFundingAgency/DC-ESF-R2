using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearnerContact1
    {
        public int LearnerContactId { get; set; }
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? LocType { get; set; }
        public long? ContType { get; set; }
        public string PostCode { get; set; }
        public string TelNumber { get; set; }
        public string Email { get; set; }
    }
}
