using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarMonthDT : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_CalendarMonth_DT";

        public bool IsWarning => false;

        public string ErrorMessage => "CalendarMonth must be an integer (whole number). Please adjust the value and resubmit the file.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CalendarMonth) && int.TryParse(model.CalendarMonth, out var month);
        }
    }
}
