using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IStorageController
    {
        Task<bool> StoreData(
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper supplementaryDataWrapper,
            CancellationToken cancellationToken);

        Task<bool> StoreValidationOnly(
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken);
    }
}