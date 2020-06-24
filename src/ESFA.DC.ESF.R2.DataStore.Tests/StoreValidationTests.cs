using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.Logging.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.DataStore.Tests
{
    public class StoreValidationTests
    {
        [Fact]
        public async Task StoreAsync()
        {
            var cancellationToken = CancellationToken.None;
            var connection = new SqlConnection();

            var sourceFileId = 1;

            IEnumerable<ValidationErrorModel> inputModels = new List<ValidationErrorModel>
            {
                new ValidationErrorModel
                {
                    ULN = "1",
                    ConRefNumber = "ConRef1",
                    DeliverableCode = "1"
                },
                new ValidationErrorModel
                {
                    ULN = "2",
                    ConRefNumber = "ConRef2",
                    DeliverableCode = "2"
                }
            };

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.GetNowUtc()).Returns(new DateTime(2019, 8, 1));

            var datastoreQueryMock = new Mock<IDataStoreQueryExecutionService>();
            datastoreQueryMock.Setup(x => x.BulkCopy(DataStoreConstants.TableNameConstants.EsfSuppDataValidationError, It.IsAny<IEnumerable<ValidationError>>(), connection, It.IsAny<SqlTransaction>(), cancellationToken)).Returns(Task.CompletedTask);

            await NewService(dateTimeProviderMock.Object, datastoreQueryMock.Object).StoreAsync(connection, It.IsAny<SqlTransaction>(), sourceFileId, inputModels, cancellationToken);

            datastoreQueryMock.VerifyAll();
        }

        [Fact]
        public void BuildModelFromEntity()
        {
            var model = new ValidationErrorModel
            {
                IsWarning = true,
                RuleName = "RuleName",
                ErrorMessage = "ErrorMessage",
                ConRefNumber = "ConRefNumber",
                DeliverableCode = "DeliverableCode",
                CalendarYear = "CalendarYear",
                CalendarMonth = "CalendarMonth",
                CostType = "CostType",
                ReferenceType = "ReferenceType",
                Reference = "Reference",
                ULN = "ULN",
                ProviderSpecifiedReference = "ProviderSpecifiedReference",
                Value = "Value",
                LearnAimRef = "LearnAimRef",
                SupplementaryDataPanelDate = "SupplementaryDataPanelDate",
            };

            var expectedModel = new ValidationError
            {
                Severity = "W",
                RuleId = "RuleName",
                ErrorMessage = "ErrorMessage",
                CreatedOn = new DateTime(2019, 8, 1),
                ConRefNumber = "ConRefNumber",
                DeliverableCode = "DeliverableCode",
                CalendarYear = "CalendarYear",
                CalendarMonth = "CalendarMonth",
                CostType = "CostType",
                ReferenceType = "ReferenceType",
                Reference = "Reference",
                ULN = "ULN",
                ProviderSpecifiedReference = "ProviderSpecifiedReference",
                Value = "Value",
                LearnAimRef = "LearnAimRef",
                SupplementaryDataPanelDate = "SupplementaryDataPanelDate",
                SourceFileId = 1
            };

            NewService().BuildModelFromEntity(model, new DateTime(2019, 8, 1), 1).Should().BeEquivalentTo(expectedModel);
        }

        private StoreValidation NewService(IDateTimeProvider dateTimeProvider = null, IDataStoreQueryExecutionService dataStoreQueryExecutionService = null)
        {
            return new StoreValidation(dateTimeProvider, dataStoreQueryExecutionService, Mock.Of<ILogger>());
        }
    }
}
