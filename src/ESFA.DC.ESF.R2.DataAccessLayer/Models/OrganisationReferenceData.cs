using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.ReferenceData;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Models
{
    public class OrganisationReferenceData : IOrganisationReferenceData
    {
        public int Ukprn { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> ConRefNumbers { get; set; }
    }
}
