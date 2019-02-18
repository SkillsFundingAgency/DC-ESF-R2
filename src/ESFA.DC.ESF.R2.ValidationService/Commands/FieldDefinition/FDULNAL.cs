using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDULNAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 10;

        public string ErrorName => "FD_ULN_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The ULN must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.ULN?.Trim()) || model.ULN.Length <= FieldLength;
        }
    }
}
