using System.Collections.Generic;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using ESFA.DC.ESF.R2.ValidationService.Commands.CrossRecord;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class ValidatorModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            RegisterFileLevelValidators(containerBuilder);
            RegisterCrossRecordValidators(containerBuilder);
            RegisterBusinessRuleValidators(containerBuilder);
            RegisterFieldDefinitionValidators(containerBuilder);
        }

        private static void RegisterFileLevelValidators(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FileNameRule08>().As<IFileLevelValidator>();
            containerBuilder.RegisterType<ConRefNumberRule01>().As<IFileLevelValidator>();
            containerBuilder.RegisterType<ConRefNumberRule02>().As<IFileLevelValidator>();

            containerBuilder.Register(c => new List<IFileLevelValidator>(c.Resolve<IEnumerable<IFileLevelValidator>>())).As<IList<IFileLevelValidator>>();
        }

        private static void RegisterCrossRecordValidators(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<Duplicate01>().As<ICrossRecordValidator>();

            containerBuilder.Register(c => new List<ICrossRecordValidator>(c.Resolve<IEnumerable<ICrossRecordValidator>>()))
                .As<IList<ICrossRecordValidator>>();
        }

        private static void RegisterBusinessRuleValidators(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<CalendarMonthRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<CalendarYearCalendarMonthRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<CalendarYearCalendarMonthRule02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<CalendarYearCalendarMonthRule03>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<CalendarYearRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<CostTypeRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<CostTypeRule02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<DeliverableCodeRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<DeliverableCodeRule02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ProviderSpecifiedReferenceRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ReferenceRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ReferenceRule03>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ReferenceTypeRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ReferenceTypeRule02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ULNRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ULNRule02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ULNRule03>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ULNRule04>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ValueRule01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<ValueRule02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<LearnAimRef01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<LearnAimRef02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<LearnAimRef03>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<LearnAimRef04>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<LearnAimRef05>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<LearnAimRef06>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<SupplementaryDataPanelDate01>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<SupplementaryDataPanelDate02>().As<IBusinessRuleValidator>();
            containerBuilder.RegisterType<SupplementaryDataPanelDate03>().As<IBusinessRuleValidator>();

            containerBuilder.Register(c => new List<IBusinessRuleValidator>(c.Resolve<IEnumerable<IBusinessRuleValidator>>()))
                .As<IList<IBusinessRuleValidator>>();
        }

        private static void RegisterFieldDefinitionValidators(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<FDCalendarMonthAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCalendarMonthDT>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCalendarMonthMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCalendarYearAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCalendarYearDT>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCalendarYearMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDConRefNumberAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDConRefNumberMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCostTypeAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDCostTypeMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDDeliverableCodeAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDDeliverableCodeMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDProviderSpecifiedReferenceAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDReferenceAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDReferenceMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDReferenceTypeAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDReferenceTypeMA>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDULNAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDULNDT>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDValueAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDLearnAimRefAL>().As<IFieldDefinitionValidator>();
            containerBuilder.RegisterType<FDSupplementaryDataPanelDateDT>().As<IFieldDefinitionValidator>();

            containerBuilder.Register(c => new List<IFieldDefinitionValidator>(c.Resolve<IEnumerable<IFieldDefinitionValidator>>()))
                .As<IList<IFieldDefinitionValidator>>();
        }
    }
}
