﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDConRefNumberMA : BaseValidationRule, IFieldDefinitionValidator
    {
        public FDConRefNumberMA(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_ConRefNumber_MA;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.ConRefNumber?.Trim());
        }
    }
}
