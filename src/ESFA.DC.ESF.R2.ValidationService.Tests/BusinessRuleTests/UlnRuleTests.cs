using System;
using System.Collections.Generic;
using System.Threading;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class UlnRuleTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule01CatchesEmptyULNs()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = null
            };

            var rule = new ULNRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule01PassesPopulatedULNs()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 1990909009
            };

            var rule = new ULNRule01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule02PassesULNsFoundInLookup()
        {
            var referenceRepo = new Mock<IReferenceDataService>();
            referenceRepo
                .Setup(x => x.GetUlnLookup(It.IsAny<IList<long?>>(), It.IsAny<CancellationToken>()))
                .Returns(new HashSet<long> { 1990909009 });

            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 1990909009
            };

            var rule = new ULNRule02(_messageServiceMock.Object, referenceRepo.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule03CatchesULNsForDatesOlderThan2MonthsAgo()
        {
            var date = DateTime.Now.AddMonths(-6);

            var monthYearHelperMock = new Mock<IMonthYearHelper>();
            monthYearHelperMock
                .Setup(m => m.GetFirstOfCalendarMonthDateTime(date.Year, date.Month))
                .Returns(new DateTime(date.Year, date.Month, 1));

            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 9999999999,
                CalendarYear = date.Year,
                CalendarMonth = date.Month
            };

            var dateNow = DateTime.Now;
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(m => m.GetNowUtc()).Returns(dateNow);
            dateTimeProvider.Setup(m => m.ConvertUtcToUk(dateNow)).Returns(dateNow);

            var rule = new ULNRule03(_messageServiceMock.Object, dateTimeProvider.Object, monthYearHelperMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule03PassesULNsForMonthsAfer2MonthsAgo()
        {
            var date = DateTime.Now;

            var monthYearHelperMock = new Mock<IMonthYearHelper>();
            monthYearHelperMock
                .Setup(m => m.GetFirstOfCalendarMonthDateTime(date.Year, date.Month))
                .Returns(date);

            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 9999999999,
                CalendarYear = date.Year,
                CalendarMonth = date.Month
            };

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(m => m.GetNowUtc()).Returns(date);
            dateTimeProvider.Setup(m => m.ConvertUtcToUk(date)).Returns(date);

            var rule = new ULNRule03(_messageServiceMock.Object, dateTimeProvider.Object, monthYearHelperMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule04CatchesULNsWhenNotRequired()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Other",
                ULN = 1990909009
            };

            var rule = new ULNRule04(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ULNRule04PassesEmptyULNsWhenNotRequired()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Other",
                ULN = null
            };

            var rule = new ULNRule04(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}
