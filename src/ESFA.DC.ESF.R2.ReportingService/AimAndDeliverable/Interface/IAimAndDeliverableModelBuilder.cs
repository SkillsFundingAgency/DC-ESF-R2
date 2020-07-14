using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Interface
{
    public interface IAimAndDeliverableModelBuilder
    {
        IEnumerable<AimAndDeliverableReportRow> Build(
            IEsfJobContext EsfJobContext,
            ICollection<LearningDelivery> learningDeliveries,
            ICollection<DPOutcome> dpOutcomes,
            ICollection<ESFLearningDeliveryDeliverablePeriod> deliverablePeriods,
            ICollection<ESFDPOutcome> esfDpOutcomes,
            ICollection<LARSLearningDelivery> larsLearningDeliveries,
            ICollection<FCSDeliverableCodeMapping> fcsDeliverableCodeMappings);
    }
}
