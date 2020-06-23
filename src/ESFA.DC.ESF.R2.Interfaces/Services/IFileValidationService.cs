using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IFileValidationService
    {
        Task<SupplementaryDataWrapper> GetFile(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFileModel,
            CancellationToken cancellationToken);

        Task<SupplementaryDataWrapper> RunFileValidators(
            ISourceFileModel sourceFileModel,
            SupplementaryDataWrapper wrapper);
    }
}