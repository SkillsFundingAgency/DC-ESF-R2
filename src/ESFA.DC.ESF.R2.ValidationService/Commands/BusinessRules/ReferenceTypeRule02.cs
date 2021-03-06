﻿using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ReferenceTypeRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        public ReferenceTypeRule02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.ReferenceType_02;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var referenceType = model.ReferenceType?.Trim();
            var costType = model.CostType?.Trim();

            var grantRecipientCostTypes = new List<string> { ValidationConstants.CostType_Grant, ValidationConstants.CostType_GrantManagement };

            var unitCostTypes = new List<string> { ValidationConstants.CostType_UnitCost, ValidationConstants.CostType_UnitCostDeduction };

            var errorCondition =
                (referenceType.CaseInsensitiveEquals(ValidationConstants.ReferenceType_Invoice) && !costType.CaseInsensitiveEquals(ValidationConstants.CostType_OtherCosts))
                ||
                (referenceType.CaseInsensitiveEquals(ValidationConstants.ReferenceType_GrantRecipient) && !grantRecipientCostTypes.Any(g => g.CaseInsensitiveEquals(costType)))
                ||
                (referenceType.CaseInsensitiveEquals(ValidationConstants.ReferenceType_LearnRefNumber) && !unitCostTypes.Any(uct => uct.CaseInsensitiveEquals(costType)) && !costType.CaseInsensitiveEquals(ValidationConstants.CostType_AuthorisedClaims))
                ||
                (referenceType.CaseInsensitiveEquals(ValidationConstants.ReferenceType_Other) && !unitCostTypes.Any(uct => uct.CaseInsensitiveEquals(costType)));

            return !errorCondition;
        }
    }
}
