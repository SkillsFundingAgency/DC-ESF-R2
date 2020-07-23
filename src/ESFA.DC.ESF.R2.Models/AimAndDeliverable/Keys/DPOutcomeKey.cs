using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ESF.R2.Models.AimAndDeliverable
{
    public struct DPOutcomeKey
    {
        public DPOutcomeKey(string learnRefNumber, string outcomeType, long outcomeCode, DateTime outcomeStartDate)
        {
            LearnRefNumber = learnRefNumber;
            OutcomeType = outcomeType;
            OutcomeCode = outcomeCode;
            OutcomeStartDate = outcomeStartDate;
        }

        public string LearnRefNumber { get; }

        public DateTime OutcomeStartDate { get; }

        public string OutcomeType { get; }

        public long OutcomeCode { get; }

        public override int GetHashCode()
            =>
            (
                OutcomeCode,
                OutcomeStartDate,
                OutcomeType?.ToUpper(),
                LearnRefNumber?.ToUpper()).GetHashCode();
    }
}
