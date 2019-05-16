using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarMonthAL : BaseValidationRule, IFieldDefinitionValidator
    {
        private const int FieldLength = 2;

        public FDCalendarMonthAL(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "FD_CalendarMonth_AL";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            var month = model.CalendarMonth;

            return !string.IsNullOrEmpty(month)
                      && !string.IsNullOrWhiteSpace(month)
                      && month.Length <= FieldLength;
        }
    }
}
