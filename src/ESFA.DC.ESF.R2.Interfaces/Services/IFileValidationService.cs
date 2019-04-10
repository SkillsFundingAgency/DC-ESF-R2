using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Services
{
    public interface IFileValidationService
    {
        Task<SupplementaryDataWrapper> GetFile(
            JobContextModel jobContextModel,
            SourceFileModel sourceFileModel,
            CancellationToken cancellationToken);

        SupplementaryDataWrapper RunFileValidators(
            SourceFileModel sourceFileModel,
            SupplementaryDataWrapper wrapper);
    }
}