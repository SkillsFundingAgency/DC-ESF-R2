using Autofac;
using ESFA.DC.ESF.R2.DataStore;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.ReportingService;
using ESFA.DC.ESF.R2.Service;
using ESFA.DC.ESF.R2.ValidationService;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class ControllerModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ServiceController>().As<IServiceController>();
            containerBuilder.RegisterType<ReportingController>().As<IReportingController>();
            containerBuilder.RegisterType<ValidationController>().As<IValidationController>();
            containerBuilder.RegisterType<StorageController>().As<IStorageController>();
        }
    }
}
