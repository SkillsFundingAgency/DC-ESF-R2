using System;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Validation;

namespace ESFA.DC.ESF.R2.ValidationService.Helpers
{
    public class MonthYearHelper : IMonthYearHelper
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public MonthYearHelper(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public DateTime GetCalendarDateTime(int? calendarYear, int? calendarMonth)
        {
            if (calendarYear == null || calendarMonth == null || calendarMonth < 1 || calendarMonth > 12)
            {
                return DateTime.MinValue;
            }

            return new DateTime(calendarYear.Value, calendarMonth.Value, _dateTimeProvider.GetNowUtc().Day);
        }
    }
}
