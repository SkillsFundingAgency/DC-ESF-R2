using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class ULNTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDULNALCatchesTooLongULNs()
        {
            var model = new SupplementaryDataLooseModel
            {
                ULN = "12345678901"
            };
            var rule = new FDULNAL(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Fact]
        public void FDULNALPassesValidULNs()
        {
            var model = new SupplementaryDataLooseModel
            {
                ULN = "1234567890"
            };
            var rule = new FDULNAL(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Fact]
        public void FDULNDTCatchesInvalidULNs()
        {
            var model = new SupplementaryDataLooseModel
            {
                ULN = "0"
            };
            var rule = new FDULNDT(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Fact]
        public void FDULNDTPassesValidULNs()
        {
            var model = new SupplementaryDataLooseModel
            {
                ULN = "1234567890"
            };
            var rule = new FDULNDT(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}