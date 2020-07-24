using ESFA.DC.ESF.R2.Interfaces.ReferenceData;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Models
{
    public class ReferenceDataVersions : IReferenceDataVersions
    {
        public string LarsVersion { get; set; }

        public string PostcodeVersion { get; set; }

        public string OrganisationVersion { get; set; }
    }
}
