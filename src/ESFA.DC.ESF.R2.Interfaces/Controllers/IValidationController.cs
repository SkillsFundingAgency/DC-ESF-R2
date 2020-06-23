using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IValidationController
    {
        bool RejectFile { get; }

        Task ValidateData(
            SupplementaryDataWrapper wrapper,
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken);
    }
}