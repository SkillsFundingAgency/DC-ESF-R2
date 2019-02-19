using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ULNRule04 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The ULN is not required for the selected ReferenceType.";

        public string ErrorName => "ULN_04";

        public bool IsWarning => true;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model.ReferenceType == Constants.ReferenceType_LearnRefNumber || model.ULN == null;
        }
    }
}
