using Autofac;
using ESFA.DC.FileService;
using ESFA.DC.FileService.Config.Interface;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class IOModule : Module
    {
        private readonly IAzureStorageFileServiceConfiguration _azureStorageFileServiceConfig;

        public IOModule(IAzureStorageFileServiceConfiguration azureStorageFileServiceConfig)
        {
            _azureStorageFileServiceConfig = azureStorageFileServiceConfig;
        }

        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterInstance(_azureStorageFileServiceConfig).As<IAzureStorageFileServiceConfiguration>();

            containerBuilder.RegisterType<AzureStorageFileService>().As<IFileService>();
        }
    }
}
