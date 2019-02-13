using ESFA.DC.ESF.R2.Interfaces.Config;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public class ReferenceDataConfig : IReferenceDataConfig
    {
        public string LARSConnectionString { get; set; }

        public string PostcodesConnectionString { get; set; }

        public string OrganisationConnectionString { get; set; }

        public string ULNConnectionString { get; set; }
    }
}
