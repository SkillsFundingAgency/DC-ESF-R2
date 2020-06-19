using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Strategies
{
    public interface ITaskStrategy
    {
        int Order { get; }

        bool IsMatch(string taskName);

        Task Execute(
            IEsfJobContext esfJobContext,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken);
    }
}