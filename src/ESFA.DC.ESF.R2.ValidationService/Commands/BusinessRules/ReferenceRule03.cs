using System;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ReferenceRule03 : BaseValidationRule, IBusinessRuleValidator
    {
        public ReferenceRule03(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "Reference_03";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !model.ReferenceType.CaseInsensitiveEquals(Constants.ReferenceType_LearnRefNumber)
                   || (string.IsNullOrEmpty(model.Reference) || Constants.ReferenceRule03Regex.IsMatch(model.Reference));
        }
    }
}