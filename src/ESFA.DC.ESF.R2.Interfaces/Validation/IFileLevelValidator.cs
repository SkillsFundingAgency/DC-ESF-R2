using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IFileLevelValidator : IBaseValidator
    {
        bool RejectFile { get; }

        bool Execute(SourceFileModel sourceFileModel, SupplementaryDataLooseModel model);
    }
}