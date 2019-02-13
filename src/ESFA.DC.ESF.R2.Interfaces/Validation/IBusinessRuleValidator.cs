using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IBusinessRuleValidator : IBaseValidator
    {
        bool Execute(SupplementaryDataModel model);
    }
}