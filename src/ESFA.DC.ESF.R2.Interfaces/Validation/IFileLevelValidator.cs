using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IFileLevelValidator : IBaseValidator
    {
        bool RejectFile { get; }

        Task<bool> IsValid(ISourceFileModel sourceFileModel, SupplementaryDataLooseModel model);
    }
}