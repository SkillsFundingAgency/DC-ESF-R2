using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Service.Helpers;
using ESFA.DC.ESF.R2.ValidationService.Helpers;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class HelpersModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FileHelper>().As<IFileHelper>();
            containerBuilder.RegisterType<TaskHelper>().As<ITaskHelper>();
            containerBuilder.RegisterType<PeriodHelper>().As<IPeriodHelper>();
            containerBuilder.RegisterType<FcsCodeMappingHelper>().As<IFcsCodeMappingHelper>();
        }
    }
}
