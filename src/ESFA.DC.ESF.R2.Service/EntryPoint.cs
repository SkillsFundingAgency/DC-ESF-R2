﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.JobContextManager.Model;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Service
{
    public class EntryPoint
    {
        private readonly ILogger _logger;

        private readonly IServiceController _controller;

        public EntryPoint(
            IServiceController controller,
            ILogger logger)
        {
            _controller = controller;
            _logger = logger;
        }

        public async Task<bool> Callback(JobContextMessage jobContextMessage, CancellationToken cancellationToken)
        {
            _logger.LogInfo("ESF callback invoked");

            var tasks = jobContextMessage.Topics[jobContextMessage.TopicPointer].Tasks;
            if (!tasks.Any())
            {
                _logger.LogInfo("ESF. No tasks to run.");
                return true;
            }

            if (cancellationToken.IsCancellationRequested)
            {
                return true;
            }

            await _controller.RunTasks(jobContextMessage, tasks, cancellationToken);

            return true;
        }
    }
}
