﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ULNRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        public ULNRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.ULN_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !(model.ReferenceType == ValidationConstants.ReferenceType_LearnRefNumber && model.ULN == null);
        }
    }
}
