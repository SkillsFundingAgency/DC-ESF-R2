namespace ESFA.DC.ESF.R2.ValidationService.Tests.HelperTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ESFA.DC.ESF.R2.ValidationService.Helpers;
    using FluentAssertions;
    using FluentAssertions.Extensions;
    using Moq;
    using Xunit;

    public class MonthHelperTests
    {
        [Theory]
        [InlineData(2020, 2)]
        public void MonthHelperTestValidYearMonth(int? calendarYear, int? calendarMonth)
        {
            var monthYearHelper = new MonthYearHelper();

            var getDate = monthYearHelper.GetCalendarDateTime(calendarYear, calendarMonth);

            getDate.Should().Be(1.February(2020).At(23, 59, 59));
        }

        [Theory]
        [InlineData(2020, 0)]
        [InlineData(2020, 13)]
        public void MonthHelperTestInvalidMonth(int? calendarYear, int? calendarMonth)
        {
            var monthYearHelper = new MonthYearHelper();

            var getDate = monthYearHelper.GetCalendarDateTime(calendarYear, calendarMonth);

            getDate.Should().Be(1.January(0001));
        }

        [Theory]
        [InlineData(2020, null)]
        [InlineData(null, 2)]
        public void MonthHelperTestNullValues(int? calendarYear, int? calendarMonth)
        {
            var monthYearHelper = new MonthYearHelper();

            var getDate = monthYearHelper.GetCalendarDateTime(calendarYear, calendarMonth);

            getDate.Should().Be(1.January(0001));
        }
    }
}
