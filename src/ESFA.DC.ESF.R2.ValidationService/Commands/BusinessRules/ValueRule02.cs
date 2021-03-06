﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ValueRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        public ValueRule02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.Value_02;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !(model.CostType.CaseInsensitiveEquals(ValidationConstants.CostType_UnitCost) || model.CostType.CaseInsensitiveEquals(ValidationConstants.CostType_UnitCostDeduction))
                        || model.Value == null;
        }
    }
}
