using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef01 : IBusinessRuleValidator
    {
        public string ErrorName => "LearnAimRef_01";

        public bool IsWarning => false;

        public string ErrorMessage => "LearnAimRef must be returned for the selected DeliverableCode.";

        public bool IsValid(SupplementaryDataModel model)
        {
            return (!model.DeliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_NR01)
                        && !model.DeliverableCode.CaseInsensitiveEquals(Constants.DeliverableCode_RQ01))
                   || !string.IsNullOrEmpty(model.LearnAimRef?.Trim());
        }
    }
}