using Autofac;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.FundingData.Database.EF;
using ESFA.DC.ESF.FundingData.Database.EF.Interfaces;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Database.EF.Interfaces;
using ESFA.DC.ESF.R2.Service.Config;
using ESFA.DC.ESF.R2.Service.Config.Interfaces;
using ESFA.DC.ESF.R2.Stateless.Handlers;
using ESFA.DC.ESF.R2.Stateless.Modules;
using ESFA.DC.FileService.Config;
using ESFA.DC.ILR.DataService.Services;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.ReferenceData.FCS.Model;
using ESFA.DC.ReferenceData.FCS.Model.Interface;
using ESFA.DC.ReferenceData.LARS.Model;
using ESFA.DC.ReferenceData.LARS.Model.Interface;
using ESFA.DC.ReferenceData.Organisations.Model;
using ESFA.DC.ReferenceData.Organisations.Model.Interface;
using ESFA.DC.ReferenceData.Postcodes.Model;
using ESFA.DC.ReferenceData.Postcodes.Model.Interface;
using ESFA.DC.ReferenceData.ULN.Model;
using ESFA.DC.ReferenceData.ULN.Model.Interface;
using ESFA.DC.ServiceFabric.Common.Config.Interface;
using ESFA.DC.ServiceFabric.Common.Modules;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.R2.Stateless
{
    public class DIComposition
    {
        public static ContainerBuilder BuildContainer(IServiceFabricConfigurationService serviceFabricConfigurationService)
        {
            var container = new ContainerBuilder();

            var statelessServiceConfiguration = serviceFabricConfigurationService.GetConfigSectionAsStatelessServiceConfiguration();

            var versionInfo = serviceFabricConfigurationService.GetConfigSectionAs<Service.Config.VersionInfo>("VersionSection");
            container.RegisterInstance(versionInfo).As<IVersionInfo>().SingleInstance();

            container.RegisterModule(new StatelessServiceModule(statelessServiceConfiguration));

            var azureStorageFileServiceConfiguration = serviceFabricConfigurationService.GetConfigSectionAs<AzureStorageFileServiceConfiguration>("AzureStorageFileServiceConfiguration");
            container.RegisterModule(new IOModule(azureStorageFileServiceConfiguration));

            container.RegisterModule<StorageModule>();
            container.RegisterModule<ReportsModule>();
            container.RegisterModule<ValidatorModule>();
            container.RegisterModule<StrategyModule>();
            container.RegisterModule<ServicesModule>();
            container.RegisterModule<ControllerModule>();
            container.RegisterModule<DataModule>();
            container.RegisterModule<HelpersModule>();
            container.RegisterModule<SerializationModule>();

            RegisterPersistence(container, serviceFabricConfigurationService);

            container.RegisterType<JobContextMessageHandler>().As<IMessageHandler<JobContextMessage>>();
            container.RegisterType<DateTimeProvider.DateTimeProvider>().As<IDateTimeProvider>();
            return container;
        }

        private static void RegisterPersistence(ContainerBuilder containerBuilder, IServiceFabricConfigurationService serviceFabricConfigurationService)
        {
            var ilrConfig = serviceFabricConfigurationService.GetConfigSectionAs<ILRConfiguration>("ILRSection");
            containerBuilder.RegisterInstance(ilrConfig).As<IILRConfiguration>().SingleInstance();
            containerBuilder.RegisterModule(new DependencyInjectionModule
            {
                Configuration = ilrConfig
            });

            var esfConfig = serviceFabricConfigurationService.GetConfigSectionAs<ESFConfiguration>("ESFSection");
            containerBuilder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<ESFR2Context>()
                    .UseSqlServer(esfConfig.ESFR2ConnectionString)
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .Options;
                return new ESFR2Context(options);
            }).As<IESFR2Context>();
            containerBuilder.RegisterInstance(esfConfig).As<IESFConfiguration>().SingleInstance();

            containerBuilder.Register(c =>
            {
                var options = new DbContextOptionsBuilder<ESFFundingDataContext>()
                    .UseSqlServer(
                        esfConfig.ESFFundingConnectionString,
                        providerOptions => providerOptions.CommandTimeout(300))
                    .Options;

                return new ESFFundingDataContext(options);
            }).As<IESFFundingDataContext>();

            var fcsConfig = serviceFabricConfigurationService.GetConfigSectionAs<FCSConfiguration>("FCSSection");
            containerBuilder.RegisterInstance(fcsConfig).As<IFCSConfiguration>().SingleInstance();

            containerBuilder.Register(c =>
                {
                    var optionsBuilder = new DbContextOptionsBuilder<FcsContext>();
                    optionsBuilder.UseSqlServer(
                        fcsConfig.FCSConnectionString,
                        providerOptions => providerOptions.CommandTimeout(60));
                    return new FcsContext(optionsBuilder.Options);
                })
                .As<IFcsContext>();

            var referenceData = serviceFabricConfigurationService.GetConfigSectionAs<ReferenceDataConfig>("ReferenceDataSection");
            containerBuilder.RegisterInstance(referenceData).As<IReferenceDataConfig>().SingleInstance();

            containerBuilder.Register(c =>
            {
                var referenceDataConfig = c.Resolve<IReferenceDataConfig>();
                var optionsBuilder = new DbContextOptionsBuilder<LarsContext>();
                optionsBuilder.UseSqlServer(
                    referenceDataConfig.LARSConnectionString,
                    providerOptions => providerOptions.CommandTimeout(60));
                return new LarsContext(optionsBuilder.Options);
            }).As<ILARSContext>();

            containerBuilder.Register(c =>
            {
                var referenceDataConfig = c.Resolve<IReferenceDataConfig>();
                var optionsBuilder = new DbContextOptionsBuilder<PostcodesContext>();
                optionsBuilder.UseSqlServer(
                    referenceDataConfig.PostcodesConnectionString,
                    providerOptions => providerOptions.CommandTimeout(60));
                return new PostcodesContext(optionsBuilder.Options);
            }).As<IPostcodesContext>();

            containerBuilder.Register(c =>
            {
                var referenceDataConfig = c.Resolve<IReferenceDataConfig>();
                var orgOptionsBuilder = new DbContextOptionsBuilder<OrganisationsContext>();
                orgOptionsBuilder.UseSqlServer(
                    referenceDataConfig.OrganisationConnectionString,
                    providerOptions => providerOptions.CommandTimeout(60));
                return new OrganisationsContext(orgOptionsBuilder.Options);
            }).As<IOrganisationsContext>();

            containerBuilder.Register(c =>
            {
                var referenceDataConfig = c.Resolve<IReferenceDataConfig>();
                var optionsBuilder = new DbContextOptionsBuilder<UlnContext>();
                optionsBuilder.UseSqlServer(
                    referenceDataConfig.ULNConnectionString,
                    providerOptions => providerOptions.CommandTimeout(60));
                return new UlnContext(optionsBuilder.Options);
            }).As<IUlnContext>();
        }
    }
}