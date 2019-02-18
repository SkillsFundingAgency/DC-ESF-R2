using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDStaffNameAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 100;

        public string ErrorName => "FD_StaffName_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The StaffName must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.StaffName?.Trim()) || model.StaffName.Length <= FieldLength;
        }
    }
}
