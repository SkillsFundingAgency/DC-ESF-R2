﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCostTypeMA : BaseValidationRule, IFieldDefinitionValidator
    {
        public FDCostTypeMA(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_CostType_MA;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CostType?.Trim());
        }
    }
}
