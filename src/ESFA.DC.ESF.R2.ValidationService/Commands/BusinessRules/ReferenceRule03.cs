using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ReferenceRule03 : BaseValidationRule, IBusinessRuleValidator
    {
        public ReferenceRule03(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.Reference_03;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !model.ReferenceType.CaseInsensitiveEquals(ValidationConstants.ReferenceType_LearnRefNumber)
                   || (string.IsNullOrEmpty(model.Reference) || ValidationConstants.ReferenceRule03Regex.IsMatch(model.Reference));
        }
    }
}