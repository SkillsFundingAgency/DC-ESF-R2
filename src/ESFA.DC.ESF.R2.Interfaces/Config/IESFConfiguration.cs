namespace ESFA.DC.ESF.R2.Interfaces.Config
{
    public interface IESFConfiguration
    {
        string ESFR2ConnectionString { get; }

        string ESFFundingConnectionString { get; }
    }
}
