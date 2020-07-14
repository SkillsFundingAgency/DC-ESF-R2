using System;

namespace ESFA.DC.ESF.R2.Models.AimAndDeliverable
{
    public class ESFDPOutcome
    {
        public string LearnRefNumber { get; set; }

        public DateTime OutcomeStartDate { get; set; }

        public string OutcomeType { get; set; }

        public long OutcomeCode { get; set; }

        public DateTime? OutDateForProgression { get; set; }
    }
}
