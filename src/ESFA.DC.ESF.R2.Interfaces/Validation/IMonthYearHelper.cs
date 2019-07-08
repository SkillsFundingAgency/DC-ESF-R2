using System;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IMonthYearHelper
    {
        DateTime GetCalendarDateTime(int? calendarYear, int? calendarMonth);
    }
}