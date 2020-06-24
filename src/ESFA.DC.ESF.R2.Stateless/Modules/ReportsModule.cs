using System.Collections.Generic;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.ReportingService.Reports;
using ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class ReportsModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<ValidationResultReport>().As<IValidationResultReport>();
            containerBuilder.RegisterType<ValidationErrorReport>().As<IValidationReport>().InstancePerLifetimeScope();
            containerBuilder.Register(c => new List<IValidationReport>(c.Resolve<IEnumerable<IValidationReport>>())).As<IList<IValidationReport>>();
            containerBuilder.RegisterType<FundingReport>().As<IModelReport>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<FundingSummaryReport>().As<IModelReport>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AimAndDeliverableReport>().As<IModelReport>().InstancePerLifetimeScope();
            containerBuilder.Register(c => new List<IModelReport>(c.Resolve<IEnumerable<IModelReport>>())).As<IList<IModelReport>>();
        }
    }
}
