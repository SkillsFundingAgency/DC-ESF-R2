using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
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
    public class StoreESFTests
    {
        [Fact]
        public async Task StoreAsync()
        {
            var cancellationToken = CancellationToken.None;
            var connection = new SqlConnection();

            var sourceFileId = 1;

            IEnumerable<SupplementaryDataModel> inputModels = new List<SupplementaryDataModel>
            {
                new SupplementaryDataModel
                {
                    ULN = 1,
                },
                new SupplementaryDataModel
                {
                    ULN = 2,
                }
            };

            var datastoreQueryMock = new Mock<IDataStoreQueryExecutionService>();
            datastoreQueryMock.Setup(x => x.BulkCopy(DataStoreConstants.TableNameConstants.EsfSuppData, It.IsAny<IEnumerable<SupplementaryData>>(), connection, It.IsAny<SqlTransaction>(), cancellationToken)).Returns(Task.CompletedTask);

            await NewService(datastoreQueryMock.Object).StoreAsync(connection, It.IsAny<SqlTransaction>(), sourceFileId, inputModels, cancellationToken);

            datastoreQueryMock.VerifyAll();
        }

        [Fact]
        public void BuildModelFromEntity()
        {
            var model = new SupplementaryDataModel
            {
                ConRefNumber = "ConRefNumber",
                DeliverableCode = "DeliverableCode",
                CalendarYear = 2019,
                CalendarMonth = 1,
                CostType = "CostType",
                ReferenceType = "ReferenceType",
                Reference = "Reference",
                ULN = 1,
                ProviderSpecifiedReference = "ProviderSpecifiedReference",
                Value = 1,
                LearnAimRef = "LearnAimRef",
                SupplementaryDataPanelDate = new DateTime(2019, 8, 1),
            };

            var expectedModel = new SupplementaryData
            {
                ConRefNumber = "ConRefNumber",
                DeliverableCode = "DeliverableCode",
                CalendarYear = 2019,
                CalendarMonth = 1,
                CostType = "CostType",
                ReferenceType = "ReferenceType",
                Reference = "Reference",
                ULN = 1,
                ProviderSpecifiedReference = "ProviderSpecifiedReference",
                Value = 1,
                LearnAimRef = "LearnAimRef",
                SupplementaryDataPanelDate = new DateTime(2019, 8, 1),
                SourceFileId = 1
            };

            NewService().BuildModelFromEntity(model, 1).Should().BeEquivalentTo(expectedModel);
        }

        private StoreESF NewService(IDataStoreQueryExecutionService dataStoreQueryExecutionService = null)
        {
            return new StoreESF(dataStoreQueryExecutionService, Mock.Of<ILogger>());
        }
    }
}
