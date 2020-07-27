using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Models.FundingSummary
{
    public class ReferenceDataVersions : IReferenceDataVersions
    {
        public string LarsVersion { get; set; }

        public string PostcodeVersion { get; set; }

        public string OrganisationVersion { get; set; }
    }
}
