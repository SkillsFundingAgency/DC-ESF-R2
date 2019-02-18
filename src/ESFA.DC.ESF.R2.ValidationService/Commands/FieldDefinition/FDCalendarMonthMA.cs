using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarMonthMA : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_CalendarMonth_MA";

        public bool IsWarning => false;

        public string ErrorMessage =>
            "The CalendarMonth is mandatory. Please resubmit the file including the appropriate value.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CalendarMonth?.Trim());
        }
    }
}
