using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.BusinessRuleTests
{
    public class DeliverableCodeTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void DeliverableCodeRule01CatchesInvalidDeliverableCodes()
        {
            var model = new SupplementaryDataModel
            {
                DeliverableCode = "Invalid Code"
            };

            var rule = new DeliverableCodeRule01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(model));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void DeliverableCodeRule01PassesValidDeliverableCodes()
        {
            var validCodes = new List<string>
            {
                "ST01", "CG01", "CG02", "SD01", "SD02", "SD10", "NR01", "RQ01",
                "PG01", "PG03", "PG04", "PG05", "SU15", "SU21", "SU22", "SU23", "SU24"
            };

            foreach (var code in validCodes)
            {
                var model = new SupplementaryDataModel
                {
                    DeliverableCode = code
                };

                var rule = new DeliverableCodeRule01(_messageServiceMock.Object);

                Assert.True(rule.IsValid(model));
            }
        }
    }
}
