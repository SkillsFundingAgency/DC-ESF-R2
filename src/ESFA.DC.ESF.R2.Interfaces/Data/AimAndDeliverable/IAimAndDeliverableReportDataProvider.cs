using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.Interfaces.Data.AimAndDeliverable
{
    public interface IAimAndDeliverableReportDataProvider
    {
        Task<ICollection<LearningDelivery>> GetLearningDeliveriesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<ESFLearningDeliveryDeliverablePeriod>> GetDeliverablePeriodsAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<DPOutcome>> GetDPOutcomesAsync(int ukprn, CancellationToken cancellationToken);

        Task<ICollection<ESFDPOutcome>> GetESFDPOutcomesAsync(int ukprn, CancellationToken cancellationToken);
    }
}
