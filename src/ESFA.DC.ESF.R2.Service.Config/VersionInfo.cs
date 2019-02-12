using ESFA.DC.ESF.R2.Interfaces.Config;

namespace ESFA.DC.ESF.R2.Service.Config
{
    public sealed class VersionInfo : IVersionInfo
    {
        public string ServiceReleaseVersion { get; set; }
    }
}
