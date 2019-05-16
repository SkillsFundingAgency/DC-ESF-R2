using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        public ValueRule02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "Value_02";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !(model.CostType.CaseInsensitiveEquals(Constants.CostType_UnitCost) || model.CostType.CaseInsensitiveEquals(Constants.CostType_UnitCostDeduction))
                        || model.Value == null;
        }
    }
}
