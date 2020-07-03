using System;

namespace ESFA.DC.ILR.DataService.Models
{
    public class DpOutcome
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public string OutType { get; set; }

        public int OutCode { get; set; }

        public DateTime OutStartDate { get; set; }

        public DateTime? OutEndDate { get; set; }

        public DateTime OutCollDate { get; set; }
    }
}