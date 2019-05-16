using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class ConRefNumberRuleTests : BaseTest
    {
        [Trait("Category", "ValidationService")]
        [Theory]
        [InlineData("")]
        [InlineData("ESF-5000")]
        public void ConRefNumber02PassesForEmptyOrValidConRefNumber(string conRefNumber)
        {
            var model = new SupplementaryDataModel
            {
                ConRefNumber = conRefNumber
            };

            var rule = new ConRefNumberRule02(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Fact]
        public void ConRefNumber02FailsWithInvalidConRefNumber()
        {
            var model = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-4999"
            };

            var rule = new ConRefNumberRule02(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }
    }
}