using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearCalendarMonthRule01 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CalendarYearCalendarMonthRule01(
            IValidationErrorMessageService errorMessageService,
            IDateTimeProvider dateTimeProvider,
            IReferenceDataService referenceDataService)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
            _dateTimeProvider = dateTimeProvider;
        }

        public override string ErrorName => "CalendarYearCalendarMonth_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            if (model.CalendarMonth == null || model.CalendarYear == null)
            {
                return false;
            }

            var year = _dateTimeProvider.GetNowUtc().Year;
            var month = _dateTimeProvider.GetNowUtc().Month;
            return model.CalendarYear < year || (model.CalendarYear == year && model.CalendarMonth <= month);
        }
    }
}