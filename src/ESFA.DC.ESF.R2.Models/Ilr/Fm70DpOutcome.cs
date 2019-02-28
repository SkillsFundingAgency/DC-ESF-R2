using System;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class Fm70DpOutcome
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public int OutCode { get; set; }

        public string OutType { get; set; }

        public DateTime OutStartDate { get; set; }

        public DateTime? OutcomeDateForProgression { get; set; }
    }
}