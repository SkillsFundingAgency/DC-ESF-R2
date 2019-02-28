using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class LearnerModel
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public long? Uln { get; set; }

        public int? PmUkPrn { get; set; }

        public string CampId { get; set; }

        public IEnumerable<LearningDeliveryModel> LearningDeliveries { get; set; }

        public IEnumerable<ProviderSpecLearnerMonitoringModel> ProviderSpecLearnerMonitorings { get; set; }
    }
}