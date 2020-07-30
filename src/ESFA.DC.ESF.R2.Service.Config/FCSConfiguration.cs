using ESFA.DC.ESF.R2.Service.Config.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public class FCSConfiguration : IFCSConfiguration
    {
        public string FCSConnectionString { get; set; }
    }
}
