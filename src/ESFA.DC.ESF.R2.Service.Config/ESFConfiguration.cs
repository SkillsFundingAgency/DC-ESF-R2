using ESFA.DC.ESF.R2.Interfaces.Config;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public class ESFConfiguration : IESFConfiguration
    {
        public string ESFR2ConnectionString { get; set; }

        public string ESFFundingConnectionString { get; set; }
    }
}
