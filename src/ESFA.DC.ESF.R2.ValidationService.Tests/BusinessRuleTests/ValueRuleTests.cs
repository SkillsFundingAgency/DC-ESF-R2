using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class ValueRuleTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void ValueRule01CatchesEmptyValueForCostTypeRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Grant",
                Value = null
            };

            var rule = new ValueRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ValueRule01PassesNonEmptyValueForCostTypeRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Grant",
                Value = 19.99M
            };

            var rule = new ValueRule01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ValueRule02CatchesValueForCostTypeNotRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Unit Cost",
                Value = 19.99M
            };

            var rule = new ValueRule02(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ValueRule02PassesEmptyValueForCostTypeNotRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Unit Cost",
                Value = null
            };

            var rule = new ValueRule02(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}
