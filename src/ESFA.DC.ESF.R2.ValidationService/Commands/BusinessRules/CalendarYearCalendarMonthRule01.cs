using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearCalendarMonthRule01 : IBusinessRuleValidator
    {
        private readonly IReferenceDataCache _referenceDataCache;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CalendarYearCalendarMonthRule01(
            IDateTimeProvider dateTimeProvider,
            IReferenceDataCache referenceDataCache)
        {
            _referenceDataCache = referenceDataCache;
            _dateTimeProvider = dateTimeProvider;
        }

        public string ErrorMessage => "The CalendarMonth you have submitted data for cannot be in the future for the current collection period.";

        public string ErrorName => "CalendarYearCalendarMonth_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            if (model.CalendarMonth == null || model.CalendarYear == null)
            {
                return false;
            }

            return model.CalendarYear <= _dateTimeProvider.GetNowUtc().Year && ESFConstants.MonthToCollection[model.CalendarMonth.Value] <= _referenceDataCache.CurrentPeriod;
        }
    }
}
