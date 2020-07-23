using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ESF.R2.Models.AimAndDeliverable
{
    public class DPOutcome
    {
        public string LearnRefNumber { get; set; }

        public DateTime OutcomeStartDate { get; set; }

        public string OutcomeType { get; set; }

        public long OutcomeCode { get; set; }

        public DateTime? OutEndDate { get; set; }

        public DateTime OutCollDate { get; set; }
    }
}
