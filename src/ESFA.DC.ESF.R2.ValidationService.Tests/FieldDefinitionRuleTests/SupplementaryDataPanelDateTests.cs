using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class SupplementaryDataPanelDateTests : BaseTest
    {
        [Theory]
        [InlineData("123456789")]
        [InlineData(null)]
        public void FDSupplementaryDataPanelDateDTFailsNotValidDate(string panelDate)
        {
            var model = new SupplementaryDataLooseModel
            {
                SupplementaryDataPanelDate = panelDate
            };
            var rule = new FDSupplementaryDataPanelDateDT(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        public void FDSupplementaryDataPanelDateDTPassesValidDate()
        {
            var model = new SupplementaryDataLooseModel
            {
                SupplementaryDataPanelDate = "31/12/2018"
            };
            var rule = new FDSupplementaryDataPanelDateDT(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}