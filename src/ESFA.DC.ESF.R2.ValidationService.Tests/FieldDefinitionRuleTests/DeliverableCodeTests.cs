using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class DeliverableCodeTests
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDDeliverableCodeALCatchesTooLongDeliverableCodes()
        {
            var model = new SupplementaryDataLooseModel
            {
                DeliverableCode = "12345678901"
            };
            var rule = new FDDeliverableCodeAL();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDDeliverableCodeALPassesValidDeliverableCodes()
        {
            var model = new SupplementaryDataLooseModel
            {
                DeliverableCode = "1234567890"
            };
            var rule = new FDDeliverableCodeAL();

            Assert.True(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDDeliverableCodeMACatchesEmptyDeliverableCodes()
        {
            var model = new SupplementaryDataLooseModel
            {
                DeliverableCode = null
            };
            var rule = new FDDeliverableCodeMA();

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDDeliverableCodeMAPassesValidDeliverableCodes()
        {
            var model = new SupplementaryDataLooseModel
            {
                DeliverableCode = "1234567890"
            };
            var rule = new FDDeliverableCodeMA();

            Assert.True(rule.IsValid(model));
        }
    }
}