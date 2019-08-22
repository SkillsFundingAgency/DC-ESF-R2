using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using ESFA.DC.ESF.ILR1819.ReferenceData;
using ESFA.DC.ESF.ILR1920.ReferenceData;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Stateless.Mappers;
using ESFA.DC.JobContext.Interface;
using ESFA.DC.JobContextManager.Interface;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Stateless.Handlers
{
    public class JobContextMessageHandler : IMessageHandler<JobContextMessage>
    {
        private const string Ilr1920CollectionName = "ILR1920";

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
            using (var childLifetimeScope = GetChildLifetimeScope(jobContextMessage))
            {
                _logger.LogInfo("ESF R2 callback invoked");

                var jobContextModel = JobContextMapper.MapJobContextToModel(jobContextMessage);
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

        public ILifetimeScope GetChildLifetimeScope(JobContextMessage jobContextMessage)
        {
            return _lifetimeScope.BeginLifetimeScope(c =>
            {
                if (jobContextMessage.KeyValuePairs[JobContextMessageKey.CollectionName].ToString() == Ilr1920CollectionName)
                {
                    c.RegisterType<Ilr1920ReferenceDataCacheService>().As<IIlrReferenceDataCacheService>();
                }
                else
                {
                    c.RegisterType<Ilr1819ReferenceDataCacheService>().As<IIlrReferenceDataCacheService>();
                }
            });
        }
    }
}
