using Autofac;
using ESFA.DC.ESF.R2.DataAccessLayer;
using ESFA.DC.ESF.R2.DataAccessLayer.Mappers;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SourceFileModelMapper>().As<ISourceFileModelMapper>();
            containerBuilder.RegisterType<SupplementaryDataModelMapper>().As<ISupplementaryDataModelMapper>();

            containerBuilder.RegisterType<EsfRepository>().As<IEsfRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ReferenceDataRepository>().As<IReferenceDataRepository>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FCSRepository>().As<IFCSRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<ValidationErrorMessageCache>().As<IValidationErrorMessageCache>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ReferenceDataCache>().As<IReferenceDataCache>().InstancePerLifetimeScope();
        }
    }
}
