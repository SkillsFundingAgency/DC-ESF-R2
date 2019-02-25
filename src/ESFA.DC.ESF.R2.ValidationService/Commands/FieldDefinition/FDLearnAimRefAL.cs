using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDLearnAimRefAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 8;

        public string ErrorName => "FD_LearnAimRef_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The LearnAimRef must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrWhiteSpace(model.LearnAimRef) && model.LearnAimRef.Length <= FieldLength;
        }
    }
}
