using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarMonthRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        public CalendarMonthRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.CalendarMonth_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model.CalendarMonth != null && model.CalendarMonth >= ValidationConstants.CalendarMonthMinValue && model.CalendarMonth <= ValidationConstants.CalendarMonthMaxValue;
        }
    }
}
