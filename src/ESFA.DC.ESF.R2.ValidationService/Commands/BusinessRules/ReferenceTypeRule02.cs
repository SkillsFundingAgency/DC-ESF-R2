using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ReferenceTypeRule02 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The ReferenceType is not valid for the selected CostType. Please refer to the ESF Supplementary Data supporting documentation for further information.";

        public string ErrorName => "ReferenceType_02";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var referenceType = model.ReferenceType?.Trim();
            var costType = model.CostType?.Trim();

            var grantRecipientCostTypes = new List<string> { Constants.CostType_Grant, Constants.CostType_GrantManagement };

            var unitCostTypes = new List<string> { Constants.CostType_UnitCost, Constants.CostType_UnitCostDeduction };

            var errorCondition =
                (referenceType.CaseInsensitiveEquals(Constants.ReferenceType_Invoice) && !costType.CaseInsensitiveEquals(Constants.CostType_OtherCosts))
                ||
                (referenceType.CaseInsensitiveEquals(Constants.ReferenceType_GrantRecipient) && !grantRecipientCostTypes.Any(g => g.CaseInsensitiveEquals(costType)))
                ||
                (referenceType.CaseInsensitiveEquals(Constants.ReferenceType_LearnRefNumber) && !unitCostTypes.Any(uct => uct.CaseInsensitiveEquals(costType)) && !costType.CaseInsensitiveEquals(Constants.CostType_AuthorisedClaims))
                ||
                referenceType.CaseInsensitiveEquals(Constants.ReferenceType_Other) && !unitCostTypes.Any(uct => uct.CaseInsensitiveEquals(costType));

            return !errorCondition;
        }
    }
}
