using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model
{
    public class AimAndDeliverableReportRow
    {
        public LearningDelivery LearningDelivery { get; set; }

        public ESFLearningDeliveryDeliverablePeriod DeliverablePeriod { get; set; }

        public LARSLearningDelivery LarsLearningDelivery { get; set; }

        public ESFDPOutcome ESFDPOutcome { get; set; }

        public DPOutcome DPOutcome { get; set; }

        public string DeliverableName { get; set; }

        public string ReportMonth { get; set; }
    }
}