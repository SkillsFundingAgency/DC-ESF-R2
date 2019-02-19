using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IFieldDefinitionValidator : IBaseValidator
    {
        bool IsValid(SupplementaryDataLooseModel model);
    }
}