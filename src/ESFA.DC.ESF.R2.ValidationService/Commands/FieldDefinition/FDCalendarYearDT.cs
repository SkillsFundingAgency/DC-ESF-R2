using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarYearDT : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_CalendarYear_DT";

        public bool IsWarning => false;

        public string ErrorMessage => "CalendarYear must be an integer (whole number). Please adjust the value and resubmit the file.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CalendarYear) && int.TryParse(model.CalendarYear, out var year);
        }
    }
}
