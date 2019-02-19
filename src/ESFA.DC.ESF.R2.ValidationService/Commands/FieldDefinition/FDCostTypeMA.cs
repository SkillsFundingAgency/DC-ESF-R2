using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCostTypeMA : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_CostType_MA";

        public bool IsWarning => false;

        public string ErrorMessage =>
            "The CostType is mandatory. Please resubmit the file including the appropriate value.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CostType?.Trim());
        }
    }
}
