using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class ValueRuleTests
    {
        [Fact]
        public void ValueRule01CatchesEmptyValueForCostTypeRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Grant",
                Value = null
            };

            var rule = new ValueRule01();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void ValueRule01PassesNonEmptyValueForCostTypeRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Grant",
                Value = 19.99M
            };

            var rule = new ValueRule01();

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        public void ValueRule02CatchesValueForCostTypeNotRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Unit Cost",
                Value = 19.99M
            };

            var rule = new ValueRule02();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void ValueRule02PassesEmptyValueForCostTypeNotRequiringOne()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Unit Cost",
                Value = null
            };

            var rule = new ValueRule02();

            Assert.True(rule.IsValid(model));
        }
    }
}
