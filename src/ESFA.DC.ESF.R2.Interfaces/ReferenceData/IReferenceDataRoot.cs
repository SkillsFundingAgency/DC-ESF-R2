using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Enum;

namespace ESFA.DC.ESF.R2.Interfaces.ReferenceData
{
    public interface IReferenceDataRoot
    {
        IDictionary<CollectionYear, IIlrFile> IlrFileForCollectionYear { get; }

        IDictionary<string, IEsfSuppDataFile> EsfSuppDataFileForConRefNumbers { get; }

        IOrganisationReferenceData OrganisationReferenceData { get; }

        IReferenceDataVersions ReferenceDataVersions { get; }
    }
}
