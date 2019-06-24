using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.FieldDefinitionRuleTests
{
    public class SupplementaryDataPanelDateTests : BaseTest
    {
        [Trait("Category", "ValidationService")]
        [Theory]
        [InlineData("123456789")]
        public void FDSupplementaryDataPanelDateDTFailsNotValidDate(string panelDate)
        {
            var model = new SupplementaryDataLooseModel
            {
                SupplementaryDataPanelDate = panelDate
            };
            var rule = new FDSupplementaryDataPanelDateDT(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Trait("Category", "ValidationService")]
        [Theory]
        [InlineData("31/12/2018")]
        [InlineData(null)]
        public void FDSupplementaryDataPanelDateDTPassesValidDate(string panelDate)
        {
            var model = new SupplementaryDataLooseModel
            {
                SupplementaryDataPanelDate = panelDate
            };
            var rule = new FDSupplementaryDataPanelDateDT(_messageServiceMock.Object);

            Assert.True(rule.IsValid(model));
        }
    }
}