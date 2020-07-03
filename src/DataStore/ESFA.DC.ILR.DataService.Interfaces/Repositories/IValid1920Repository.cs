using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Repositories
{
    public interface IValid1920Repository
    {
        Task<IEnumerable<LearnerDetails>> GetLearnerDetails(
            int ukPrn,
            int fundingModel,
            IEnumerable<string> conRefNums,
            CancellationToken cancellationToken);

        Task<IEnumerable<string>> GetLearnerConRefNumbers(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<DpOutcome>> GetDPOutcomes(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<LearningDeliveryFam>> GetLearningDeliveryFams(int ukPrn, string famType, CancellationToken cancellationToken);

        Task<IEnumerable<ProviderSpecLearnerMonitoring>> GetLearnerMonitorings(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<ProviderSpecDeliveryMonitoring>> GetDeliveryMonitorings(int ukPrn, CancellationToken cancellationToken);
    }
}