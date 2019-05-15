using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDProviderSpecifiedReferenceAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int FieldLength = 200;

        public FDProviderSpecifiedReferenceAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "FD_ProviderSpecifiedReference_AL";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.ProviderSpecifiedReference?.Trim()) || model.ProviderSpecifiedReference.Length <= FieldLength;
        }
    }
}
