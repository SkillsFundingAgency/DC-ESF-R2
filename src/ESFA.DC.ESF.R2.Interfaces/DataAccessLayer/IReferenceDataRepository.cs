using System.Collections.Generic;
using System.Threading;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.ULN.Model;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataRepository
    {
        string GetPostcodeVersion(CancellationToken cancellationToken);

        string GetLarsVersion(CancellationToken cancellationToken);

        string GetOrganisationVersion(CancellationToken cancellationToken);

        string GetProviderName(int ukPrn, CancellationToken cancellationToken);

        IList<UniqueLearnerNumber> GetUlnLookup(IList<long?> searchUlns, CancellationToken cancellationToken);

        IList<LARS_LearningDelivery> GetLarsLearningDelivery(IList<string> learnAimRefs, CancellationToken cancellationToken);
    }
}