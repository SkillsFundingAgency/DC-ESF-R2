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
        public void FDLearnAimRefALFailsLearnAimRefTooLong(string learnAimRef)
        {
            var model = new SupplementaryDataLooseModel
            {
                LearnAimRef = learnAimRef
            };
            var rule = new FDLearnAimRefAL(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Theory]
        [InlineData("12345678")]
        [InlineData(null)]
        public void FDLearnAimRefALPassesLearnAimRefCorrect(string learnAimRef)
        {
            var model = new SupplementaryDataLooseModel
            {
                LearnAimRef = learnAimRef
            };
            var rule = new FDLearnAimRefAL(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}