using System;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class SupplementaryDataPanelDateTests
    {
        [Fact]
        public void SupplementaryDataPanelDate01FailsDateInFuture()
        {
            var suppData = new SupplementaryDataModel
            {
                SupplementaryDataPanelDate = new DateTime(2018, 9, 2)
            };

            var date = new DateTime(2018, 9, 1);
            var mock = new Mock<IDateTimeProvider>();
            mock
                .Setup(m => m.GetNowUtc())
                .Returns(date);
            mock
                .Setup(m => m.ConvertUtcToUk(It.IsAny<DateTime>()))
                .Returns(date);

            var rule = new SupplementaryDataPanelDate01(mock.Object);

            Assert.False(rule.IsValid(suppData));
        }

        [Fact]
        public void SupplementaryDataPanelDate01PassesNoSupplementaryDataPanelDate()
        {
            var suppData = new SupplementaryDataModel
            {
                SupplementaryDataPanelDate = null
            };

            var mock = new Mock<IDateTimeProvider>();

            var rule = new SupplementaryDataPanelDate01(mock.Object);

            Assert.True(rule.IsValid(suppData));
        }

        [Fact]
        public void SupplementaryDataPanelDate01PassesSupplementaryDataPanelDateNotInFuture()
        {
            var suppData = new SupplementaryDataModel
            {
                SupplementaryDataPanelDate = new DateTime(2018, 9, 1)
            };

            var date = new DateTime(2018, 9, 1);
            var mock = new Mock<IDateTimeProvider>();
            mock
                .Setup(m => m.GetNowUtc())
                .Returns(date);
            mock
                .Setup(m => m.ConvertUtcToUk(It.IsAny<DateTime>()))
                .Returns(date);

            var rule = new SupplementaryDataPanelDate01(mock.Object);

            Assert.True(rule.IsValid(suppData));
        }
    }
}