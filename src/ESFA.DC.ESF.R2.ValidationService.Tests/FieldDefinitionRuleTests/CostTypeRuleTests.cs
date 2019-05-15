using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class CostTypeRuleTests
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCostTypeALCatchesTooLongCostTypes()
        {
            var model = new SupplementaryDataLooseModel
            {
                CostType = "123456789012345678901"
            };
            var rule = new FDCostTypeAL();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCostTypeALPassesValidCostTypes()
        {
            var model = new SupplementaryDataLooseModel
            {
                CostType = "12345678901234567890"
            };
            var rule = new FDCostTypeAL();

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCostTypeMACatchesEmptyCostTypes()
        {
            var model = new SupplementaryDataLooseModel
            {
                CostType = null
            };
            var rule = new FDCostTypeMA();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDCostTypeMAPassesValidCostTypes()
        {
            var model = new SupplementaryDataLooseModel
            {
                CostType = "12345678901234567890"
            };
            var rule = new FDCostTypeMA();

            Assert.True(rule.IsValid(model));
        }
    }
}