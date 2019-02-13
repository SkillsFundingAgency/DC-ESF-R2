using System.Threading;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IValidationController
    {
        bool RejectFile { get; }

        void ValidateData(
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken);
    }
}