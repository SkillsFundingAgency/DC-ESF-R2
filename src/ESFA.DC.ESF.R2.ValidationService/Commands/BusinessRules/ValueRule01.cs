using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _costTypesRequiringValue = new List<string>
        {
            Constants.CostType_OtherCosts,
            Constants.CostType_Grant,
            Constants.CostType_GrantManagement,
            Constants.CostType_AuthorisedClaims
        };

        public ValueRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "Value_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !_costTypesRequiringValue.Any(ct => ct.CaseInsensitiveEquals(model.CostType)) || model.Value != null;
        }
    }
}
