using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Repositories
{
    public interface IFm701516Repository
    {
        Task<IEnumerable<FM70PeriodisedValues>> Get1516PeriodisedValues(
            int ukPrn,
            CancellationToken cancellationToken);
    }
}