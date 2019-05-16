using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef02 : BaseValidationRule, IBusinessRuleValidator
    {
        public LearnAimRef02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "LearnAimRef_02";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return (!model.DeliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_CG01)
                        && !model.DeliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_CG02)
                        && !model.DeliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_SD01)
                        && !model.DeliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_SD02))
                   || string.IsNullOrEmpty(model.LearnAimRef?.Trim());
        }
    }
}