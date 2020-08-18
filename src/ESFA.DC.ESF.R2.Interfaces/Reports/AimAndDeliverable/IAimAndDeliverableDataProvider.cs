﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable
{
    public interface IAimAndDeliverableDataProvider
    {
        Task<ICollection<LearningDelivery>> GetLearningDeliveriesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<DPOutcome>> GetDpOutcomesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<ESFLearningDeliveryDeliverablePeriod>> GetDeliverablePeriodsAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<ESFDPOutcome>> GetEsfDpOutcomesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<LARSLearningDelivery>> GetLarsLearningDeliveriesAsync(ICollection<LearningDelivery> learningDeliveries, CancellationToken cancellationToken);

        Task<ICollection<FCSDeliverableCodeMapping>> GetFcsDeliverableCodeMappingsAsync(CancellationToken cancellationToken);
    }
}