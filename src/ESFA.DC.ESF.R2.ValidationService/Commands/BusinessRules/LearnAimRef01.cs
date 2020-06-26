using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef01 : BaseValidationRule, IBusinessRuleValidator
    {
        public LearnAimRef01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.LearnAimRef_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return (!model.DeliverableCode.CaseInsensitiveEquals(ValidationConstants.DeliverableCode_NR01)
                        && !model.DeliverableCode.CaseInsensitiveEquals(ValidationConstants.DeliverableCode_RQ01))
                   || !string.IsNullOrEmpty(model.LearnAimRef?.Trim());
        }
    }
}