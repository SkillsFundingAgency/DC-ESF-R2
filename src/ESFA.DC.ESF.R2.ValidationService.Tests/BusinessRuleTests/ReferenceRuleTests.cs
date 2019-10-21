using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class ReferenceRuleTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void ProviderSpecifiedReferenceRule01CatchesRegexViolations()
        {
            var invalidCharacters = "|~?";

            var model = new SupplementaryDataModel
            {
                ProviderSpecifiedReference = invalidCharacters
            };

            var rule = new ProviderSpecifiedReferenceRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ProviderSpecifiedReferenceRule01PassesValidReferences()
        {
            var model = new SupplementaryDataModel
            {
                ProviderSpecifiedReference = @"Aa0 .,;:~!”@#$&’()/+-<=>[]{}^£€"
            };

            var rule = new ProviderSpecifiedReferenceRule01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceRule01CatchesRegexViolations()
        {
            var invalidCharacters = "|~?";

            var model = new SupplementaryDataModel
            {
                Reference = invalidCharacters
            };

            var rule = new ReferenceRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceRule01PassesValidReferences()
        {
            var model = new SupplementaryDataModel
            {
                Reference = @"Aa0 .,;:~!”@#$&’()/+-<=>[]{}^£€"
            };

            var rule = new ReferenceRule01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceRule03CatchesRegexViolations()
        {
            var model = new SupplementaryDataModel
            {
                Reference = @"Aa0 .,;:~!”@#$&’()/+-<=>[]{}^£€",
                ReferenceType = "LearnRefNumber"
            };

            var rule = new ReferenceRule03(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceRule03IgnoresIrrelevantReferenceTypes()
        {
            var model = new SupplementaryDataModel
            {
                Reference = @"Aa0 .,;:~!”@#$&’()/+-<=>[]{}^£€",
                ReferenceType = "NotLearnRefNumber"
            };

            var rule = new ReferenceRule03(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceRule03PassesValidReferences()
        {
            var model = new SupplementaryDataModel
            {
                Reference = @"Aa0 Zz9 ",
                ReferenceType = "LearnRefNumber"
            };

            var rule = new ReferenceRule03(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceTypeRule01CatchesInvalidTypes()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Not A Valid Type"
            };

            var rule = new ReferenceTypeRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceTypeRule01PassesValidTypes()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "LearnRefNumber"
            };

            var rule = new ReferenceTypeRule01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceTypeRule02CatchesInvalidReferenceTypeCostTypeCombinations()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Other",
                CostType = "Employee ID"
            };

            var rule = new ReferenceTypeRule02(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void ReferenceTypeRule02PassesValidReferenceTypeCostTypeCombinations()
        {
            var model = new SupplementaryDataModel
            {
                ReferenceType = "Apportioned Cost",
                CostType = "Employee ID"
            };

            var rule = new ReferenceTypeRule02(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}
