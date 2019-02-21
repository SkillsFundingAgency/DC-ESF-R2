using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class ConRefNumberTests
    {
        [Fact]
        public void FDConRefNumberALCatchesTooLongConRefNumbers()
        {
            var model = new SupplementaryDataLooseModel
            {
                ConRefNumber = "123456789012345678901"
            };
            var rule = new FDConRefNumberAL();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void FDConRefNumberALPassesValidConRefNumbers()
        {
            var model = new SupplementaryDataLooseModel
            {
                ConRefNumber = "12345678901234567890"
            };
            var rule = new FDConRefNumberAL();

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        public void FDConRefNumberMACatchesEmptyConRefNumbers()
        {
            var model = new SupplementaryDataLooseModel
            {
                ConRefNumber = null
            };
            var rule = new FDConRefNumberMA();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void FDConRefNumberMAPassesValidConRefNumbers()
        {
            var model = new SupplementaryDataLooseModel
            {
                ConRefNumber = "12345678901234567890"
            };
            var rule = new FDConRefNumberMA();

            Assert.True(rule.IsValid(model));
        }
    }
}