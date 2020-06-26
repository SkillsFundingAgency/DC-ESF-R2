using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly List<string> _costTypesRequiringValue = new List<string>
        {
            ValidationConstants.CostType_OtherCosts,
            ValidationConstants.CostType_Grant,
            ValidationConstants.CostType_GrantManagement,
            ValidationConstants.CostType_AuthorisedClaims
        };

        public ValueRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.Value_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !_costTypesRequiringValue.Any(ct => ct.CaseInsensitiveEquals(model.CostType)) || model.Value != null;
        }
    }
}
