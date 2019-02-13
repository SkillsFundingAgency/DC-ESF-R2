namespace ESFA.DC.ESF.R2.Interfaces.Config
{
    public interface IReferenceDataConfig
    {
        string LARSConnectionString { get; set; }

        string PostcodesConnectionString { get; set; }

        string OrganisationConnectionString { get; set; }

        string ULNConnectionString { get; set; }
    }
}