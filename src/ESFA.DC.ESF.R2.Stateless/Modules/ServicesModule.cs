using System.Collections.Generic;
using Autofac;
using ESFA.DC.ESF.R2.DataAccessLayer.Services;
using ESFA.DC.ESF.R2.Interfaces.Builders;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.ReportingService.Services;
using ESFA.DC.ESF.R2.Service.Builders;
using ESFA.DC.ESF.R2.Service.Services;
using ESFA.DC.ESF.R2.ValidationService.Commands;
using ESFA.DC.ESF.R2.ValidationService.Helpers;
using ESFA.DC.ESF.R2.ValidationService.Services;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<SourceFileModelBuilder>().As<ISourceFileModelBuilder>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ESFProviderService>().As<IESFProviderService>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<ValidationErrorMessageService>().As<IValidationErrorMessageService>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<SupplementaryDataService>().As<ISupplementaryDataService>();
            containerBuilder.RegisterType<ILRService>().As<IILRService>();

            containerBuilder.RegisterType<FileValidationService>().As<IFileValidationService>();

            containerBuilder.RegisterType<ExcelStyleProvider>().As<IExcelStyleProvider>();

            containerBuilder.RegisterType<MonthYearHelper>().As<IMonthYearHelper>();

            containerBuilder.RegisterType<ValueProvider>().As<IValueProvider>().SingleInstance();
            containerBuilder.RegisterType<ReferenceDataService>().As<IReferenceDataService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PopulationService>().As<IPopulationService>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<AimAndDeliverableService1819>().As<IAimAndDeliverableService1819>();
            containerBuilder.RegisterType<AimAndDeliverableService1920>().As<IAimAndDeliverableService1920>();

            containerBuilder.RegisterType<ESFFundingService>().As<IESFFundingService>();
            containerBuilder.RegisterType<ReturnPeriodLookup>().As<IReturnPeriodLookup>();

            RegisterCommands(containerBuilder);
        }

        private static void RegisterCommands(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FieldDefinitionCommand>().As<ILooseValidatorCommand>();
            containerBuilder.RegisterType<BusinessRuleCommands>().As<IValidatorCommand>();
            containerBuilder.RegisterType<CrossRecordCommands>().As<IValidatorCommand>();

            containerBuilder.Register(c => new List<IValidatorCommand>(c.Resolve<IEnumerable<IValidatorCommand>>())).As<IList<IValidatorCommand>>();
        }
    }
}
