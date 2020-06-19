using System.Collections.Generic;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr;
using ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData;
using ESFA.DC.ESF.R2.Service.Strategies;

namespace ESFA.DC.ESF.R2.Stateless.Modules
{
    public class StrategyModule : Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<PersistenceStrategy>().As<ITaskStrategy>();
            containerBuilder.RegisterType<ValidationStrategy>().As<ITaskStrategy>();
            containerBuilder.Register(c => new List<ITaskStrategy>(c.Resolve<IEnumerable<ITaskStrategy>>())).As<IList<ITaskStrategy>>();

            containerBuilder.RegisterType<DataRowHelper>().As<IRowHelper>();
            containerBuilder.RegisterType<TitleRowHelper>().As<IRowHelper>();
            containerBuilder.RegisterType<SpacerRowHelper>().As<IRowHelper>();
            containerBuilder.RegisterType<TotalRowHelper>().As<IRowHelper>();
            containerBuilder.RegisterType<CumulativeRowHelper>().As<IRowHelper>();
            containerBuilder.RegisterType<MainTitleRowHelper>().As<IRowHelper>();
            containerBuilder.Register(c => new List<IRowHelper>(c.Resolve<IEnumerable<IRowHelper>>())).As<IList<IRowHelper>>();

            containerBuilder.RegisterType<CG01CommunityGrantPayment>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<CG02CommunityGrantManagementCost>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<NR01NonRegulatedActivityAuthorisedClaims>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<PG01ProgressionPaidEmploymentAdjustments>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<PG03ProgressionEducationAdjustments>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<PG04ProgressionApprenticeshipAdjustments>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<PG05ProgressionTraineeshipAdjustments>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<RQ01RegulatedLearningAuthorisedClaims>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<SD01FCSDeliverableDescription>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<SD02FCSDeliverableDescription>().As<ISupplementaryDataStrategy>();
            containerBuilder.RegisterType<ST01LearnerAssessmentAndPlanAdjustments>().As<ISupplementaryDataStrategy>();
            containerBuilder.Register(c => new List<ISupplementaryDataStrategy>(c.Resolve<IEnumerable<ISupplementaryDataStrategy>>())).As<IList<ISupplementaryDataStrategy>>();

            containerBuilder.RegisterType<NR01NonRegulatedActivityAchievementEarnings>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<NR01NonRegulatedActivityStartFunding>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<PG01ProgressionPaidEmployment>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<PG03ProgressionEducation>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<PG04ProgressionApprenticeship>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<PG05ProgressionTraineeship>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<RQ01RegulatedLearningAchievementEarnings>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<RQ01RegulatedLearningStartFunding>().As<IILRDataStrategy>();
            containerBuilder.RegisterType<ST01LearnerAssessmentAndPlan>().As<IILRDataStrategy>();
            containerBuilder.Register(c => new List<IILRDataStrategy>(c.Resolve<IEnumerable<IILRDataStrategy>>())).As<IList<IILRDataStrategy>>();
        }
    }
}
