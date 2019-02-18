﻿using System.Collections.Generic;
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

        IList<UniqueLearnerNumber> GetUlnLookup(IList<long?> searchUlns, CancellationToken cancellationToken);

        IList<LarsLearningDelivery> GetLarsLearningDelivery(IList<string> learnAimRefs, CancellationToken cancellationToken);
    }
}