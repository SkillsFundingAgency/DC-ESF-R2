using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Services
{
    public interface IFm70DataService
    {
        Task<IEnumerable<Fm70LearningDelivery>> GetLearningDeliveries(int ukPrn, bool round2, CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValues>> GetPeriodisedValuesAllYears(int ukPrn, CancellationToken cancellationToken, bool round2 = false);

        Task<IEnumerable<Fm70DpOutcome>> GetOutcomes(int ukPrn, CancellationToken cancellationToken, bool round2 = false);
    }
}