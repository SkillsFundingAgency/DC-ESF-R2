using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule02 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The Value is not required for the selected CostType";

        public string ErrorName => "Value_02";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return !(model.CostType == "Unit Cost" || model.CostType == "Unit Cost Deduction")
                        ||
                        model.Value == null;
        }
    }
}
