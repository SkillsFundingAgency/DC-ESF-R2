using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CostTypeRule01 : IBusinessRuleValidator
    {
        private readonly IList<string> _validCostTypes = new List<string>
        {
            Constants.CostTypeStaffPT,
            Constants.CostTypeStaffFT,
            "Apportioned Cost",
            "Staff Expenses",
            "Grant",
            "Grant Management",
            "Unit Cost",
            "Unit Cost Deduction",
            "Other Costs",
            "Funding Adjustment"
        };

        public string ErrorMessage => "The CostType is not valid";

        public string ErrorName => "CostType_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return _validCostTypes.Contains(model.CostType?.Trim());
        }
    }
}
