using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class EfaSfaLearnerPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Period { get; set; }
        public decimal? LnrOnProgPay { get; set; }
    }
}
