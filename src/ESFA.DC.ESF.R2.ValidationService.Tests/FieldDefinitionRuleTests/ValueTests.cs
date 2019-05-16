using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class ValueTests : BaseTest
    {
        [Trait("Category", "ValidationService")]
        [Fact]
        public void FDValueALCatchesTooLongValues()
        {
            var model = new SupplementaryDataLooseModel
            {
                Value = "1234567.123"
            };
            var rule = new FDValueAL(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Fact]
        public void FDValueALPassesValidValues()
        {
            var model = new SupplementaryDataLooseModel
            {
                Value = "123456.12"
            };
            var rule = new FDValueAL(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}