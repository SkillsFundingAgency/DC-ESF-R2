using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ReferenceTypeRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private List<string> _validReferenceTypes = new List<string>
        {
            ValidationConstants.ReferenceType_Invoice,
            ValidationConstants.ReferenceType_GrantRecipient,
            ValidationConstants.ReferenceType_LearnRefNumber,
            ValidationConstants.ReferenceType_Other
        };

        public ReferenceTypeRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.ReferenceType_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return _validReferenceTypes.Any(vrt => vrt.CaseInsensitiveEquals(model.ReferenceType?.Trim()));
        }
    }
}
