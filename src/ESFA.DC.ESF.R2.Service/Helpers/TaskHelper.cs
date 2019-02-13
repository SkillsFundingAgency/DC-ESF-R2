﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.JobContextManager.Model.Interface;

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
            IReadOnlyList<ITaskItem> tasks,
            SourceFileModel sourceFileModel,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken)
        {
            foreach (ITaskItem taskItem in tasks)
            {
                if (taskItem.SupportsParallelExecution)
                {
                    Parallel.ForEach(
                       taskItem.Tasks,
                       new ParallelOptions { CancellationToken = cancellationToken },
                       async task => { await HandleTask(supplementaryDataWrapper, task, sourceFileModel, cancellationToken); });
                }
                else
                {
                    var subTasks = taskItem.Tasks;
                    foreach (var task in subTasks)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }

                        await HandleTask(supplementaryDataWrapper, task, sourceFileModel, cancellationToken);
                    }
                }
            }
        }

        private async Task HandleTask(
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

                await handler.Execute(sourceFile, wrapper, cancellationToken);
                break;
            }
        }
    }
}
