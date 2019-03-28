using ESFA.DC.ESF.R2.Service.Config;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.ServiceFabric.Helpers.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Stateless.Tests
{
    public sealed class TestConfigurationHelper : IConfigurationHelper
    {
        public T GetSectionValues<T>(string sectionName)
        {
            switch (sectionName)
            {
                case "VersionSection":
                    return (T)(object)new VersionInfo
                    {
                        ServiceReleaseVersion = "1.2.3.4"
                    };
                //case "TopicAndTaskSection":
                //    return (T)GetTopicsAndTasks();
                case "AzureStorageSection":
                    return (T)(object)new AzureStorageOptions
                    {
                        AzureBlobConnectionString = "AzureBlobConnectionString",
                        AzureBlobContainerName = "AzureBlobContainerName"
                    };
                case "ServiceBusSettings":
                    return (T)(object)new ServiceBusOptions
                    {
                        AuditQueueName = "AuditQueueName",
                        ServiceBusConnectionString = "Endpoint=sb://xxxx.servicebus.windows.net/;SharedAccessKeyName=xxxx;SharedAccessKey=xxxx",
                        JobStatusQueueName = "JobStatusQueueName",
                        TopicName = "TopicName",
                        SubscriptionName = "DataStore"
                    };
                case "LoggerSection":
                    return (T)(object)new LoggerOptions
                    {
                        LoggerConnectionstring = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;"
                    };
                case "ILRSection":
                    return (T)(object)new ILRConfiguration
                    {
                        ILR1516ConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;",
                        ILR1617ConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;",
                        ILR1718ConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;",
                        ILR1819ConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;"
                    };
                case "ESFSection":
                    return (T)(object)new ESFConfiguration
                    {
                        ESFR2ConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;"
                    };
                case "FCSSection":
                    return (T)(object)new FCSConfiguration
                    {
                        FCSConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;"
                    };
                case "ReferenceDataSection":
                    return (T)(object)new ReferenceDataConfig
                    {
                        LARSConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;",
                        ULNConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;",
                        OrganisationConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;",
                        PostcodesConnectionString = "Server=.;Database=myDataBase;User Id=myUsername;Password = myPassword;"
                    };
            }

            return default(T);
        }
    }
}
