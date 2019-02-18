using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDReferenceAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 100;

        public string ErrorName => "FD_Reference_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The Reference must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.Reference?.Trim()) || model.Reference.Length <= FieldLength;
        }
    }
}
