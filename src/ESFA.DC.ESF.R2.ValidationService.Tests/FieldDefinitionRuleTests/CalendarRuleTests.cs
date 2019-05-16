using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class CalendarRuleTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarMonthALCatchesInvalidMonths()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarMonth = "1612"
            };
            var rule = new FDCalendarMonthAL(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarMonthALPassesValidMonths()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarMonth = "10"
            };
            var rule = new FDCalendarMonthAL(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarMonthDTCatchesInvalidMonths()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarMonth = null
            };
            var rule = new FDCalendarMonthDT(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarMonthDTPassesValidMonths()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarMonth = "10"
            };
            var rule = new FDCalendarMonthDT(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarMonthMACatchesNullMonths()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarMonth = null
            };
            var rule = new FDCalendarMonthMA(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarMonthMAPassesValidMonths()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarMonth = "10"
            };
            var rule = new FDCalendarMonthMA(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarYearALCatchesInvalidYears()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarYear = "12345"
            };
            var rule = new FDCalendarYearAL(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarYearALPassesValidYears()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarYear = "2010"
            };
            var rule = new FDCalendarYearAL(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarYearDTCatchesInvalidYears()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarYear = null
            };
            var rule = new FDCalendarYearDT(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarYearDTPassesValidYears()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarYear = "2010"
            };
            var rule = new FDCalendarYearDT(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarYearMACatchesInvalidYears()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarYear = null
            };
            var rule = new FDCalendarYearMA(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCalendarYearMAPassesValidYears()
        {
            var model = new SupplementaryDataLooseModel
            {
                CalendarYear = "2010"
            };
            var rule = new FDCalendarYearMA(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}
