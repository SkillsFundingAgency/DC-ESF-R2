using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearCalendarMonthRule01 : IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CalendarYearCalendarMonthRule01(
            IDateTimeProvider dateTimeProvider,
            IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
            _dateTimeProvider = dateTimeProvider;
        }

        public string ErrorMessage => "The CalendarMonth you have submitted data for cannot be in the future for the current collection period.";

        public string ErrorName => "CalendarYearCalendarMonth_01";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            if (model.CalendarMonth == null || model.CalendarYear == null)
            {
                return false;
            }

            return model.CalendarYear <= _dateTimeProvider.GetNowUtc().Year && ESFConstants.MonthToCollection[model.CalendarMonth.Value] <= _referenceDataService.CurrentPeriod;
        }
    }
}
