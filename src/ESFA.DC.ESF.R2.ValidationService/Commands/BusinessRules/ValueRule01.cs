using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule01 : IBusinessRuleValidator
    {
        private readonly List<string> _costTypesRequiringValue = new List<string>
        {
            "Other Costs",
            "Grant",
            "Grant Management",
            "Authorised Claims"
        };

        public string ErrorMessage => "The Value must be returned for the selected CostType.";

        public string ErrorName => "Value_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return !_costTypesRequiringValue.Any(ct => ct.CaseInsensitiveEquals(model.CostType)) || model.Value != null;
        }
    }
}
