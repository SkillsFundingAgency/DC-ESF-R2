using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Reports
{
    public interface IModelReport
    {
        Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken);

        string TaskName { get; }
    }
}