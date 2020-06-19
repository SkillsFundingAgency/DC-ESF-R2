using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IReportingController
    {
        Task FileLevelErrorReport(
            IEsfJobContext esfJobContext,
            SupplementaryDataWrapper wrapper,
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken);

        Task ProduceReports(
            IEsfJobContext esfJobContext,
            SupplementaryDataWrapper wrapper,
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken);
    }
}