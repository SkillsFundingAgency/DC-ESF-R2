using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Service;
using ESFA.DC.ESF.R2.Stateless.Mappers;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.Logging.Interfaces;

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
            }
            else if (esfJobContext.CollectionYear == AcademicYearConstants.Year2021)
            {
            }
        }
    }
}
