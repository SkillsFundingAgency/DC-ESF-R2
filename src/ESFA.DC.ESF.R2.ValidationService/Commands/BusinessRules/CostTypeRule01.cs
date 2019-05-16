using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CostTypeRule01 : BaseValidationRule, IBusinessRuleValidator
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

        public CostTypeRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "CostType_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return _validCostTypes.Any(vct => vct.CaseInsensitiveEquals(model.CostType?.Trim()));
        }
    }
}
