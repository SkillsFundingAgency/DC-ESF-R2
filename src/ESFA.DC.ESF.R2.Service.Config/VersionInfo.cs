using ESFA.DC.ESF.R2.Service.Config.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public sealed class VersionInfo : IVersionInfo
    {
        public string ServiceReleaseVersion { get; set; }
    }
}
