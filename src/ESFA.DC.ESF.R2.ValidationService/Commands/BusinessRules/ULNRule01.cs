using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ULNRule01 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The ULN must be returned.";

        public string ErrorName => "ULN_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return !(model.ReferenceType == Constants.ReferenceType_LearnRefNumber && model.ULN == null);
        }
    }
}
