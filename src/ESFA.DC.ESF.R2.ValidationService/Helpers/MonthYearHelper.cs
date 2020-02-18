using System;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Validation;

namespace ESFA.DC.ESF.R2.ValidationService.Helpers
{
    public class MonthYearHelper : IMonthYearHelper
    {
        public DateTime GetFirstOfCalendarMonthDateTime(int? calendarYear, int? calendarMonth)
        {
            if (calendarYear == null || calendarMonth == null || calendarMonth < 1 || calendarMonth > 12)
            {
                return DateTime.MinValue;
            }

            return new DateTime(calendarYear.Value, calendarMonth.Value, 1, 23, 59, 59);
        }
    }
}
