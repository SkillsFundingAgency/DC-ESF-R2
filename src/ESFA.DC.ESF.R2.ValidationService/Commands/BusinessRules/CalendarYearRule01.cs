using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        public CalendarYearRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.CalendarYear_01;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model.CalendarYear != null && model.CalendarYear >= ValidationConstants.CalendarYearMinValue && model.CalendarYear <= ValidationConstants.CalendarYearMaxValue;
        }
    }
}
