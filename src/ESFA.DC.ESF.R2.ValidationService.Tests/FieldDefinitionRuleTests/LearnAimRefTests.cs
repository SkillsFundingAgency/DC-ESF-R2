using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class LearnAimRefTests : BaseTest
    {
        [Trait("Category", "ValidationService")]
        [Theory]
        [InlineData("123456789")]
        [InlineData(null)]
        public void FDLearnAimRefALFailsLearnAimRefTooLongOrNull(string learnAimRef)
        {
            var model = new SupplementaryDataLooseModel
            {
                LearnAimRef = learnAimRef
            };
            var rule = new FDLearnAimRefAL(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void FDLearnAimRefALPassesLearnAimRefCorrect()
        {
            var model = new SupplementaryDataLooseModel
            {
                LearnAimRef = "12345678"
            };
            var rule = new FDLearnAimRefAL(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}