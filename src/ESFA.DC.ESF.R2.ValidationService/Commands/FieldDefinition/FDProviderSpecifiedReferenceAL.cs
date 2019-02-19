using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDProviderSpecifiedReferenceAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 200;

        public string ErrorName => "FD_ProviderSpecifiedReference_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The ProviderSpecifiedReference must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.ProviderSpecifiedReference?.Trim()) || model.ProviderSpecifiedReference.Length <= FieldLength;
        }
    }
}
