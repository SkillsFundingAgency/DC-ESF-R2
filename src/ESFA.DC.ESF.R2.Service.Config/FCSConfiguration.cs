using ESFA.DC.ESF.R2.Interfaces.Config;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public class FCSConfiguration : IFCSConfiguration
    {
        public string FCSConnectionString { get; set; }
    }
}
