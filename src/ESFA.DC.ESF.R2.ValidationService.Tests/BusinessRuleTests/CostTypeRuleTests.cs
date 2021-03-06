﻿using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class CostTypeRuleTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void CostTypeRule01CatchesCostTypesNotInValidList()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "I am not valid"
            };
            var rule = new CostTypeRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void CostTypeRule01PassesValidCostTypes()
        {
            var model = new SupplementaryDataModel
            {
                CostType = "Grant"
            };
            var rule = new CostTypeRule01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void CostTypeRule02CatchesInvalidCostTypeDeliverableCodeCombinations()
        {
            var model = new SupplementaryDataModel
            {
                DeliverableCode = "CG01",
                CostType = "I am not valid"
            };
            var rule = new CostTypeRule02(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void CostTypeRule02PassesValidCostTypeDeliverableCodeCombinations()
        {
            var model = new SupplementaryDataModel
            {
                DeliverableCode = "CG01",
                CostType = "Grant"
            };
            var rule = new CostTypeRule02(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}
