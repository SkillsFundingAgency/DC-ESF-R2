using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CostTypeRule01 : IBusinessRuleValidator
    {
        private readonly IList<string> _validCostTypes = new List<string>
        {
            Constants.CostType_Grant,
            Constants.CostType_GrantManagement,
            Constants.CostType_UnitCost,
            Constants.CostType_UnitCostDeduction,
            Constants.CostType_OtherCosts,
            Constants.CostType_AuthorisedClaims
        };

        public string ErrorMessage => "The CostType is not valid";

        public string ErrorName => "CostType_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return _validCostTypes.Any(vct => vct.CaseInsensitiveEquals(model.CostType?.Trim()));
        }
    }
}
