using System;
using System.Threading;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class CalendarRuleTests
    {
        [Fact]
        public void TestThatCalendarMonthRule01CatchesInvalidMonths()
        {
            var model = new SupplementaryDataModel
            {
                CalendarMonth = 16
            };
            var rule = new CalendarMonthRule01();

            Assert.False(rule.Execute(model));
        }

        [Fact]
        public void TestThatCalendarMonthRule01PassesValidMonths()
        {
            var model = new SupplementaryDataModel
            {
                CalendarMonth = 10
            };
            var rule = new CalendarMonthRule01();

            Assert.True(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearCalendarMonthRule01CatchesFutureDates()
        {
            var model = new SupplementaryDataModel
            {
                CalendarMonth = 10,
                CalendarYear = 2050
            };

            var cache = new Mock<IReferenceDataCache>();
            cache.Setup(m => m.CurrentPeriod).Returns(10);

            var dateProvider = new Mock<IDateTimeProvider>();
            dateProvider.Setup(m => m.GetNowUtc()).Returns(DateTime.Now);

            var rule = new CalendarYearCalendarMonthRule01(dateProvider.Object, cache.Object);

            Assert.False(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearCalendarMonthRule01PassesDatesNotInTheFuture()
        {
            var model = new SupplementaryDataModel
            {
                CalendarMonth = 10,
                CalendarYear = 2017
            };

            var cache = new Mock<IReferenceDataCache>();
            cache.Setup(m => m.CurrentPeriod).Returns(10);

            var dateProvider = new Mock<IDateTimeProvider>();
            dateProvider.Setup(m => m.GetNowUtc()).Returns(DateTime.Now);

            var rule = new CalendarYearCalendarMonthRule01(dateProvider.Object, cache.Object);

            Assert.True(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearCalendarMonthRule02CatchesDatesPriorToContractDate()
        {
            var allocation = new ContractAllocationCacheModel
            {
                StartDate = new DateTime(2018, 01, 01)
            };

            var mapper = new Mock<IFcsCodeMappingHelper>();
            mapper.Setup(
                    x => x.GetFcsDeliverableCode(It.IsAny<SupplementaryDataModel>(), It.IsAny<CancellationToken>()))
                .Returns(3);

            var referenceRepo = new Mock<IReferenceDataCache>();
            referenceRepo
                .Setup(x => x.GetContractAllocation(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<int?>()))
                .Returns(allocation);

            var model = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-2111",
                CalendarMonth = 10,
                CalendarYear = 2017
            };
            var rule = new CalendarYearCalendarMonthRule02(referenceRepo.Object, mapper.Object);

            Assert.False(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearCalendarMonthRule02PassesDatesInTheContractPeriod()
        {
            var allocation = new ContractAllocationCacheModel
            {
                StartDate = new DateTime(2017, 11, 01)
            };

            var mapper = new Mock<IFcsCodeMappingHelper>();
            mapper.Setup(
                    x => x.GetFcsDeliverableCode(It.IsAny<SupplementaryDataModel>(), It.IsAny<CancellationToken>()))
                .Returns(3);

            var referenceRepo = new Mock<IReferenceDataCache>();
            referenceRepo
                .Setup(x => x.GetContractAllocation(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<int?>()))
                .Returns(allocation);

            var model = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-2111",
                CalendarMonth = 11,
                CalendarYear = 2017
            };
            var rule = new CalendarYearCalendarMonthRule02(referenceRepo.Object, mapper.Object);

            Assert.True(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearCalendarMonthRule03CatchesDatesAfterTheContractDate()
        {
            var allocation = new ContractAllocationCacheModel
            {
                EndDate = new DateTime(2017, 11, 01)
            };

            var mapper = new Mock<IFcsCodeMappingHelper>();
            mapper.Setup(
                    x => x.GetFcsDeliverableCode(It.IsAny<SupplementaryDataModel>(), It.IsAny<CancellationToken>()))
                .Returns(3);

            var referenceRepo = new Mock<IReferenceDataCache>();
            referenceRepo
                .Setup(x => x.GetContractAllocation(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<int?>()))
                .Returns(allocation);

            var model = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-2111",
                CalendarMonth = 12,
                CalendarYear = 2017
            };
            var rule = new CalendarYearCalendarMonthRule03(referenceRepo.Object, mapper.Object);

            Assert.False(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearCalendarMonthRule03PassesDatesInTheContractPeriod()
        {
            var allocation = new ContractAllocationCacheModel
            {
                EndDate = new DateTime(2017, 11, 01)
            };

            var mapper = new Mock<IFcsCodeMappingHelper>();
            mapper.Setup(
                    x => x.GetFcsDeliverableCode(It.IsAny<SupplementaryDataModel>(), It.IsAny<CancellationToken>()))
                .Returns(3);

            var referenceRepo = new Mock<IReferenceDataCache>();
            referenceRepo
                .Setup(x => x.GetContractAllocation(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<int?>()))
                .Returns(allocation);

            var model = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-2111",
                CalendarMonth = 10,
                CalendarYear = 2017
            };
            var rule = new CalendarYearCalendarMonthRule03(referenceRepo.Object, mapper.Object);

            Assert.True(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearRule01CatchesYearsOutsideOfTheAllowedRange()
        {
            var model = new SupplementaryDataModel
            {
                CalendarYear = 1998
            };
            var rule = new CalendarYearRule01();

            Assert.False(rule.Execute(model));
        }

        [Fact]
        public void CalendarYearRule01PassesYearsInsideOfTheAllowedRange()
        {
            var model = new SupplementaryDataModel
            {
                CalendarYear = 2020
            };
            var rule = new CalendarYearRule01();

            Assert.True(rule.Execute(model));
        }
    }
}
