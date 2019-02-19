using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef02 : IBusinessRuleValidator
    {
        public string ErrorName => "LearnAimRef_02";

        public bool IsWarning => false;

        public string ErrorMessage => "LearnAimRef is not required for the selected DeliverableCode.";

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