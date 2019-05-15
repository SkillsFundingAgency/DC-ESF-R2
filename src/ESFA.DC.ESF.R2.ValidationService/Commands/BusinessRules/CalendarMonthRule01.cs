using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarMonthRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        public CalendarMonthRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "CalendarMonth_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model.CalendarMonth != null && model.CalendarMonth >= 1 && model.CalendarMonth <= 12;
        }
    }
}
