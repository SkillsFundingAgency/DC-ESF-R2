using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDDeliverableCodeAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 10;

        public string ErrorName => "FD_DeliverableCode_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The DeliverableCode must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.DeliverableCode?.Trim()) && model.DeliverableCode.Length <= FieldLength;
        }
    }
}
