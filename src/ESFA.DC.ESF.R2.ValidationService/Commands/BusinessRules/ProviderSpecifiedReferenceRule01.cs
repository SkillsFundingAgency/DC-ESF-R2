using System.Text.RegularExpressions;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ProviderSpecifiedReferenceRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private const string Pattern = @"^[A-Z,a-z,0-9,\s\.,;:~!”@#\$&’()\/\+-<=>\[\]\{}\^£€]*$";

        public ProviderSpecifiedReferenceRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "ProviderSpecifiedReference_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return !(!string.IsNullOrEmpty(model.ProviderSpecifiedReference) &&
                        !Regex.IsMatch(model.ProviderSpecifiedReference, Pattern));
        }
    }
}
