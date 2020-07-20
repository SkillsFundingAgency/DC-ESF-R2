using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable.DataProvider._1920
{
    public interface IIlrDataProvider
    {
        Task<ICollection<LearningDelivery>> GetLearningDeliveriesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<DPOutcome>> GetDpOutcomesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<ESFLearningDeliveryDeliverablePeriod>> GetDeliverablePeriodsAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<ESFDPOutcome>> GetEsfDpOutcomesAsync(int ukprn, CancellationToken cancellationToken);
    }
}
