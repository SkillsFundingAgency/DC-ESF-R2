namespace ESFA.DC.ESF.R2.Service.Config.Interfaces
{
    public interface IESFConfiguration
    {
        string ESFR2ConnectionString { get; }

        string ESFFundingConnectionString { get; }
    }
}
