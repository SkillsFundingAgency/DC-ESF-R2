using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Commands.CrossRecord;
using Xunit;

namespace ESFA.DC.ESF.R2.ValidationService.Tests.CrossRecordRuleTests
{
    public class DuplicateTests : BaseTest
    {
        [Fact]
        [Trait("Category", "ValidationService")]
        public void Duplicate01CatchesDuplicates()
        {
            var record = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-2108",
                DeliverableCode = "ST01",
                CalendarYear = 2018,
                CalendarMonth = 10,
                CostType = "Grant",
                ReferenceType = "LearnRefNumber",
                Reference = "100000098"
            };

            var records = new List<SupplementaryDataModel>
            {
                record,
                record
            };

            var rule = new Duplicate01(_messageServiceMock.Object);

            Assert.False(rule.IsValid(records, record));
        }

        [Fact]
        [Trait("Category", "ValidationService")]
        public void Duplicate01PassesWhenNoDuplicates()
        {
            var record = new SupplementaryDataModel
            {
                ConRefNumber = "ESF-2108",
                DeliverableCode = "ST01",
                CalendarYear = 2018,
                CalendarMonth = 10,
                CostType = "Grant",
                ReferenceType = "LearnRefNumber",
                Reference = "100000098"
            };

            var records = new List<SupplementaryDataModel>
            {
                record,
                new SupplementaryDataModel
                {
                    ConRefNumber = "ESF-2108",
                    DeliverableCode = "RQ01",
                    CalendarYear = 2018,
                    CalendarMonth = 10,
                    CostType = "Grant",
                    ReferenceType = "LearnRefNumber",
                    Reference = "100000098"
                }
            };

            var rule = new Duplicate01(_messageServiceMock.Object);

            Assert.True(rule.IsValid(records, record));
        }
    }
}
