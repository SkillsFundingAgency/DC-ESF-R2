using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataRepository
    {
        Task<string> GetPostcodeVersion(CancellationToken cancellationToken);

        Task<string> GetLarsVersion(CancellationToken cancellationToken);

        Task<string> GetOrganisationVersion(CancellationToken cancellationToken);

        string GetProviderName(int ukPrn, CancellationToken cancellationToken);

        IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken);

        Task<IDictionary<string, LarsLearningDeliveryModel>> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken);
    }
}