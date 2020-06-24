using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Helpers
{
    public interface ITaskHelper
    {
        Task ExecuteTasks(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFileModel,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken);
    }
}