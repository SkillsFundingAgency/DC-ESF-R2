using System.Collections.Generic;
using System.Threading;
using ESFA.DC.ReferenceData.LARS.Model;
using ESFA.DC.ReferenceData.ULN.Model;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataRepository
    {
        string GetPostcodeVersion(CancellationToken cancellationToken);

        string GetLarsVersion(CancellationToken cancellationToken);

        string GetOrganisationVersion(CancellationToken cancellationToken);

        string GetProviderName(int ukPrn, CancellationToken cancellationToken);

        IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken);

        IEnumerable<string> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken);
    }
}