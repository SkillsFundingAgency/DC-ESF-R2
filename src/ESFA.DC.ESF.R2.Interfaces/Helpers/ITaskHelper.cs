using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.JobContextManager.Model.Interface;

namespace ESFA.DC.ESF.R2.Interfaces.Helpers
{
    public interface ITaskHelper
    {
        Task ExecuteTasks(
            IReadOnlyList<ITaskItem> tasks,
            SourceFileModel sourceFileModel,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken);
    }
}