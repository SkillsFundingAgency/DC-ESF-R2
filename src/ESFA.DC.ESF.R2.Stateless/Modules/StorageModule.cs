using Autofac;
using ESFA.DC.BulkCopy;
using ESFA.DC.BulkCopy.Interfaces;
using ESFA.DC.ESF.R2.DataStore;
using ESFA.DC.ESF.R2.DataStore.Service;
using ESFA.DC.ESF.R2.Interfaces.DataStore;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class StorageModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<StoreFileDetails>().As<IStoreFileDetails>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<StoreESF>().As<IStoreESF>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<StoreClear>().As<IStoreClear>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<StoreESFUnitCost>().As<IStoreESFUnitCost>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<StoreValidation>().As<IStoreValidation>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<DataStoreQueryExecutionService>().As<IDataStoreQueryExecutionService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<BulkInsert>().As<IBulkInsert>().InstancePerLifetimeScope();
        }
    }
}
