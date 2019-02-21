using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models
{
    public class LarsLearningDeliveryModel
    {
        public string LearnAimRef { get; set; }

        public string LearningDeliveryGenre { get; set; }

        public IEnumerable<LarsValidityPeriod> ValidityPeriods { get; set; }
    }
}