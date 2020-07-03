using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Repositories
{
    public interface IFm701819Repository
    {
        Task<ILRFileDetails> GetFileDetails(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<Fm70LearningDelivery>> GetLearningDeliveries(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValues>> GetPeriodisedValues(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<Fm70DpOutcome>> GetOutcomes(int ukPrn, CancellationToken cancellationToken);
    }
}