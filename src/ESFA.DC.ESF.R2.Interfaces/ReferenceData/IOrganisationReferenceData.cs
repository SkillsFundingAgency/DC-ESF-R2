using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Interfaces.ReferenceData
{
    public interface IOrganisationReferenceData
    {
        int Ukprn { get; }

        string Name { get; }

        IEnumerable<string> ConRefNumbers { get; }
    }
}
