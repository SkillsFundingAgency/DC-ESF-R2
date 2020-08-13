using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Models.FundingSummary
{
    public class OrganisationReferenceData : IOrganisationReferenceData
    {
        public int Ukprn { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> ConRefNumbers { get; set; }
    }
}
