using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule02 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The Value is not required for the selected CostType";

        public string ErrorName => "Value_02";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return !(model.CostType.CaseInsensitiveEquals(Constants.CostType_UnitCost) || model.CostType.CaseInsensitiveEquals(Constants.CostType_UnitCostDeduction))
                        || model.Value == null;
        }
    }
}
