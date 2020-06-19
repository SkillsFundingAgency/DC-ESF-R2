using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Helpers
{
    public interface ITaskHelper
    {
        Task ExecuteTasks(
            IEsfJobContext esfJobContext,
            SourceFileModel sourceFileModel,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken);
    }
}