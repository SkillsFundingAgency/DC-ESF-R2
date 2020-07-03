using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Interfaces.Services
{
    public interface IFileDetailsDataService
    {
        Task<IList<ILRFileDetails>> GetFileDetailsForUkPrnAllYears(
            int ukPrn,
            CancellationToken cancellationToken,
            bool round2 = false);
    }
}