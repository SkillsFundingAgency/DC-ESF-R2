using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IIlrReferenceDataCacheService
    {
        Task PopulateCacheFromJson(JobContextModel jobContextMessage, CancellationToken cancellationToken);
    }
}