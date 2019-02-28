using System;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class DpOutcomeModel
    {
        public int UkPrn { get; set; }

        public string OutType { get; set; }

        public int OutCode { get; set; }

        public DateTime OutStartDate { get; set; }

        public DateTime? OutEndDate { get; set; }

        public DateTime OutCollDate { get; set; }
    }
}