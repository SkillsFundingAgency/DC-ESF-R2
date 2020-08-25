using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable
{
    public interface ILarsDataProvider
    {
        Task<ICollection<LARSLearningDelivery>> GetLarsLearningDeliveriesAsync(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken);
    }
}
