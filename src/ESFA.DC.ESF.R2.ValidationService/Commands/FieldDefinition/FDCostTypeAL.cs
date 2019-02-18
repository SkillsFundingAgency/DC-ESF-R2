using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCostTypeAL : IFieldDefinitionValidator
    {
        private const int FieldLength = 20;

        public string ErrorName => "FD_CostType_AL";

        public bool IsWarning => false;

        public string ErrorMessage => $"The CostType must not exceed {FieldLength} characters in length. Please adjust the value and resubmit the file.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CostType?.Trim()) && model.CostType.Length <= FieldLength;
        }
    }
}
