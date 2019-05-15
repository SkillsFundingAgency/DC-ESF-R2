using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarYearAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int FieldLength = 4;

        public FDCalendarYearAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "FD_CalendarYear_AL";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            var year = model.CalendarYear;

            return !string.IsNullOrEmpty(year)
                      && !string.IsNullOrWhiteSpace(year)
                      && year.Length <= FieldLength;
        }
    }
}
