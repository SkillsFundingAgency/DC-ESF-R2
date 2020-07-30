using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Service.Config.Interfaces;
using ESFA.DC.ESF.R2.Stateless.Mappers;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.Logging.Interfaces;
using Ilr1920AimAndDeliverableDataProvider = ESFA.DC.ESF.R2._1920.Data.AimAndDeliverable.Ilr;
using Ilr1920FundingSummaryDataProvider = ESFA.DC.ESF.R2._1920.Data.FundingSummary.Ilr;
using Ilr2021AimAndDeliverableDataProvider = ESFA.DC.ESF.R2._2021.Data.AimAndDeliverable.Ilr;
using Ilr2021FundingSummaryDataProvider = ESFA.DC.ESF.R2._2021.Data.FundingSummary.Ilr;
using IlrAimAndDeliverableDataProviderInterface = ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable.IIlrDataProvider;
using IlrFundingSummaryDataProviderInterface = ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary.IIlrDataProvider;

namespace ESFA.DC.ESF.R2.Stateless.Handlers
{
    public class JobContextMessageHandler : IMessageHandler<JobContextMessage>
    {
        private readonly ILifetimeScope _lifetimeScope;
        private readonly ILogger _logger;

        public JobContextMessageHandler(
            ILifetimeScope lifetimeScope,
            ILogger logger)
        {
            _logger = logger;
            _lifetimeScope = lifetimeScope;
        }

        public async Task<bool> HandleAsync(JobContextMessage jobContextMessage, CancellationToken cancellationToken)
        {
            var jobContextModel = JobContextMapper.MapJobContextToModel(jobContextMessage);

            using (var childLifetimeScope = _lifetimeScope.BeginLifetimeScope(s => RegisterYearSpecificServices(s, jobContextModel)))
            {
                _logger.LogInfo("ESF R2 callback invoked");

                if (!jobContextModel.Tasks.Any())
                {
                    _logger.LogInfo("ESF R2. No tasks to run.");
                    return true;
                }

                cancellationToken.ThrowIfCancellationRequested();

                var controller = childLifetimeScope.Resolve<IServiceController>();

                await controller.RunTasks(jobContextModel, cancellationToken);

                return true;
            }
        }

        private void RegisterYearSpecificServices(ContainerBuilder container, IEsfJobContext esfJobContext)
        {
            if (esfJobContext.CollectionYear == AcademicYearConstants.Year1920)
            {
                container.Register(c =>
                {
                    var ilrConfig = c.Resolve<IILRConfiguration>();

                    SqlConnection IlrSqlFunc() => new SqlConnection(ilrConfig.ILR1920ConnectionString);

                    return new Ilr1920AimAndDeliverableDataProvider.IlrDataProvider(IlrSqlFunc);
                }).As<IlrAimAndDeliverableDataProviderInterface>();

                container.Register(c =>
                {
                    var ilrConfig = c.Resolve<IILRConfiguration>();
                    var esfConfig = c.Resolve<IESFConfiguration>();
                    var returnPeriodLookup = c.Resolve<IReturnPeriodLookup>();

                    SqlConnection Ilr1819SqlFunc() => new SqlConnection(ilrConfig.ILR1819ConnectionString);
                    SqlConnection Ilr1920SqlFunc() => new SqlConnection(ilrConfig.ILR1920ConnectionString);

                    SqlConnection EsfSqlFunc() => new SqlConnection(esfConfig.ESFFundingConnectionString);

                    var connectionDictionary = new Dictionary<int, Func<SqlConnection>>
                    {
                        { AcademicYearConstants.Year2019, Ilr1920SqlFunc },
                        { AcademicYearConstants.Year2018, Ilr1819SqlFunc }
                    };

                    return new Ilr1920FundingSummaryDataProvider.IlrDataProvider(connectionDictionary, EsfSqlFunc, returnPeriodLookup);
                }).As<IlrFundingSummaryDataProviderInterface>();
            }
            else if (esfJobContext.CollectionYear == AcademicYearConstants.Year2021)
            {
                container.Register(c =>
                {
                    var ilrConfig = c.Resolve<IILRConfiguration>();

                    SqlConnection IlrSqlFunc() => new SqlConnection(ilrConfig.ILR2021ConnectionString);

                    return new Ilr2021AimAndDeliverableDataProvider.IlrDataProvider(IlrSqlFunc);
                }).As<IlrAimAndDeliverableDataProviderInterface>();

                container.Register(c =>
                {
                    var ilrConfig = c.Resolve<IILRConfiguration>();
                    var esfConfig = c.Resolve<IESFConfiguration>();
                    var returnPeriodLookup = c.Resolve<IReturnPeriodLookup>();

                    SqlConnection Ilr1819SqlFunc() => new SqlConnection(ilrConfig.ILR1819ConnectionString);
                    SqlConnection Ilr1920SqlFunc() => new SqlConnection(ilrConfig.ILR1920ConnectionString);
                    SqlConnection Ilr2021SqlFunc() => new SqlConnection(ilrConfig.ILR2021ConnectionString);

                    SqlConnection EsfSqlFunc() => new SqlConnection(esfConfig.ESFFundingConnectionString);

                    var connectionDictionary = new Dictionary<int, Func<SqlConnection>>
                    {
                        { AcademicYearConstants.Year2020, Ilr2021SqlFunc },
                        { AcademicYearConstants.Year2019, Ilr1920SqlFunc },
                        { AcademicYearConstants.Year2018, Ilr1819SqlFunc }
                    };

                    return new Ilr2021FundingSummaryDataProvider.IlrDataProvider(connectionDictionary, EsfSqlFunc, returnPeriodLookup);
                }).As<IlrFundingSummaryDataProviderInterface>();
            }
        }
    }
}
