using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.Models
{
    public class Learner
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public long? Uln { get; set; }

        public int? PmUkPrn { get; set; }

        public string CampId { get; set; }

        public IEnumerable<LearningDelivery> LearningDeliveries { get; set; }

        public IEnumerable<ProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings { get; set; }
    }
}