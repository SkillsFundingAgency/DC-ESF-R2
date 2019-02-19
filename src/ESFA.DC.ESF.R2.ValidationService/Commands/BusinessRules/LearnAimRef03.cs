using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef03 : IBusinessRuleValidator
    {
        public LearnAimRef03()
        {
        }

        public string ErrorName => "LearnAimRef_03";

        public bool IsWarning => false;

        public string ErrorMessage => "LearnAimRef does not exist on LARS.";

        public bool IsValid(SupplementaryDataModel model)
        {
            if (string.IsNullOrEmpty(model.LearnAimRef?.Trim()))
            {
                return true;
            }

            return false;
        }
    }
}