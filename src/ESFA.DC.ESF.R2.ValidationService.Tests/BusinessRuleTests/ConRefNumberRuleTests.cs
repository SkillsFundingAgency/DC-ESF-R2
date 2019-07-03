using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FileLevel;
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
            var model = new SupplementaryDataLooseModel()
            {
                ConRefNumber = conRefNumber
            };

            var rule = new ConRefNumberRule02(_messageServiceMock.Object);

            Assert.True(rule.IsValid(null, model));
        }

        [Trait("Category", "ValidationService")]
        [Fact]
        public void ConRefNumber02FailsWithInvalidConRefNumber()
        {
            var model = new SupplementaryDataLooseModel()
            {
                ConRefNumber = "ESF-4999"
            };

            var rule = new ConRefNumberRule02(_messageServiceMock.Object);

            Assert.False(rule.IsValid(null, model));
        }
    }
}