using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarMonthDT : BaseValidationRule, IFieldDefinitionValidator
    {
        public FDCalendarMonthDT(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_CalendarMonth_DT;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CalendarMonth) && int.TryParse(model.CalendarMonth, out var month);
        }
    }
}
