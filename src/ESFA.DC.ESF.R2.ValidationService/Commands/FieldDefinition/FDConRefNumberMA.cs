using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDConRefNumberMA : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_ConRefNumber_MA";

        public bool IsWarning => false;

        public string ErrorMessage => "The ConRefNumber is mandatory. Please resubmit the file including the appropriate value.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.ConRefNumber?.Trim());
        }
    }
}
