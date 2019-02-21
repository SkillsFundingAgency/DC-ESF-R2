using System;
using System.Collections.Generic;
using System.Threading;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class UlnRuleTests
    {
        [Fact]
        public void ULNRule01CatchesEmptyULNs()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = null
            };

            var rule = new ULNRule01();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void ULNRule01PassesPopulatedULNs()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 1990909009
            };

            var rule = new ULNRule01();

            Assert.True(rule.IsValid(model));
        }

        [Fact]
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

            var rule = new ULNRule02(referenceRepo.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        public void ULNRule03CatchesULNsForDatesOlderThan2MonthsAgo()
        {
            var date = DateTime.Now.AddMonths(-6);

            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 9999999999,
                CalendarYear = date.Year,
                CalendarMonth = date.Month
            };

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(m => m.GetNowUtc()).Returns(DateTime.Now);

            var rule = new ULNRule03(dateTimeProvider.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void ULNRule03PassesULNsForMonthsAfer2MonthsAgo()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber",
                ULN = 9999999999,
                CalendarYear = DateTime.Now.Year,
                CalendarMonth = DateTime.Now.Month
            };

            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(m => m.GetNowUtc()).Returns(DateTime.Now);

            var rule = new ULNRule03(dateTimeProvider.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        public void ULNRule04CatchesULNsWhenNotRequired()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Other",
                ULN = 1990909009
            };

            var rule = new ULNRule04();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void ULNRule04PassesEmptyULNsWhenNotRequired()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Other",
                ULN = null
            };

            var rule = new ULNRule04();

            Assert.True(rule.IsValid(model));
        }
    }
}
