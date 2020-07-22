using System.Collections.Generic;
using System.Data.SqlClient;
using Autofac;
using ESFA.DC.ESF.R2.Data.AimAndDeliverable.Fcs;
using ESFA.DC.ESF.R2.Data.AimAndDeliverable.Lars;
using ESFA.DC.ESF.R2.Interfaces.Config;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Interface;
using ESFA.DC.ESF.R2.ReportingService.Reports;
using ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary;
using ESFA.DC.Serialization.Interfaces;
using AimAndDeliverableReport = ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.AimAndDeliverableReport;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class ReportsModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ValidationResultReport>().As<IValidationResultReport>();
            containerBuilder.RegisterType<ValidationErrorReport>().As<IValidationReport>().InstancePerLifetimeScope();
            containerBuilder.Register(c => new List<IValidationReport>(c.Resolve<IEnumerable<IValidationReport>>()))
                .As<IList<IValidationReport>>();
            containerBuilder.RegisterType<FundingReport>().As<IModelReport>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FundingSummaryReport>().As<IModelReport>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AimAndDeliverableReport>().As<IModelReport>().InstancePerLifetimeScope();
            containerBuilder.Register(c => new List<IModelReport>(c.Resolve<IEnumerable<IModelReport>>()))
                .As<IList<IModelReport>>();

            RegisterAimAndDeliverableReport(containerBuilder);
        }

        private void RegisterAimAndDeliverableReport(ContainerBuilder builder)
        {
            builder.RegisterType<AimAndDeliverableModelBuilder>().As<IAimAndDeliverableModelBuilder>();
            builder.RegisterType<AimAndDeliverableDataProvider>().As<IAimAndDeliverableDataProvider>();

            builder.Register(c =>
            {
                var referenceDataConfig = c.Resolve<IReferenceDataConfig>();

                SqlConnection larsSqlFunc() => new SqlConnection(referenceDataConfig.LARSConnectionString);

                var jsonSerializationService = c.Resolve<IJsonSerializationService>();

                return new LarsDataProvider(larsSqlFunc, jsonSerializationService);
            }).As<ILarsDataProvider>();

            builder.Register(c =>
            {
                var fcsConfig = c.Resolve<IFCSConfiguration>();

                SqlConnection fcsSqlFunc() => new SqlConnection(fcsConfig.FCSConnectionString);

                return new FcsDataProvider(fcsSqlFunc);
            }).As<IFcsDataProvider>();
        }
    }
}
