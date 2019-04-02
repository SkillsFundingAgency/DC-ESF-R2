using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Service.Helpers
{
    public class TaskHelper : ITaskHelper
    {
        private readonly IList<ITaskStrategy> _taskHandlers;

        public TaskHelper(IList<ITaskStrategy> taskHandlers)
        {
            _taskHandlers = taskHandlers;
        }

        public async Task ExecuteTasks(
            JobContextModel jobContextModel,
            SourceFileModel sourceFileModel,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken)
        {
            var tasks = jobContextModel.Tasks;

            foreach (var task in tasks)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await HandleTask(jobContextModel, supplementaryDataWrapper, task, sourceFileModel, cancellationToken);
            }
        }

        private async Task HandleTask(
            JobContextModel jobContextModel,
            SupplementaryDataWrapper wrapper,
            string task,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            var orderedHandlers = _taskHandlers.OrderBy(t => t.Order);
            foreach (var handler in orderedHandlers)
            {
                if (!handler.IsMatch(task))
                {
                    continue;
                }

                await handler.Execute(jobContextModel, sourceFile, wrapper, cancellationToken);
                break;
            }
        }
    }
}
