using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        public CalendarYearRule01(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => "CalendarYear_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model.CalendarYear != null && model.CalendarYear >= 2019 && model.CalendarYear <= 2021;
        }
    }
}
