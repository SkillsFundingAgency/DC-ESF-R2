using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ReferenceTypeRule01 : IBusinessRuleValidator
    {
        private List<string> _validReferenceTypes = new List<string>
        {
            Constants.ReferenceType_Invoice,
            Constants.ReferenceType_GrantRecipient,
            Constants.ReferenceType_LearnRefNumber,
            Constants.ReferenceType_Other
        };

        public string ErrorMessage => "The ReferenceType is not valid.";

        public string ErrorName => "ReferenceType_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return _validReferenceTypes.Any(vrt => vrt.CaseInsensitiveEquals(model.ReferenceType?.Trim()));
        }
    }
}
