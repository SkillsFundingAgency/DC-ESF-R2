using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IValidationController
    {
        bool RejectFile { get; }

        Task ValidateData(
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken);
    }
}