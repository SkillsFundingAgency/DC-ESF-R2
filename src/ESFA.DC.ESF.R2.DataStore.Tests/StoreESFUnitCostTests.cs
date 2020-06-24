using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.Logging.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.DataStore.Tests
{
    public class StoreESFUnitCostTests
    {
        [Fact]
        public async Task StoreAsync()
        {
            var cancellationToken = CancellationToken.None;
            var connection = new SqlConnection();

            IEnumerable<SupplementaryDataModel> inputModels = new List<SupplementaryDataModel>
            {
                new SupplementaryDataModel
                {
                    ULN = 1,
                    ConRefNumber = "ConRef1",
                    DeliverableCode = "1"
                },
                new SupplementaryDataModel
                {
                    ULN = 2,
                    ConRefNumber = "ConRef2",
                    DeliverableCode = "2"
                }
            };

            var referenceDataServiceMock = new Mock<IReferenceDataService>();
            referenceDataServiceMock.SetupSequence(x => x.GetDeliverableUnitCostForDeliverableCode(It.IsAny<string>(), It.IsAny<string>())).Returns(10m).Returns(20m);

            var datastoreQueryMock = new Mock<IDataStoreQueryExecutionService>();
            datastoreQueryMock.Setup(x => x.BulkCopy(DataStoreConstants.TableNameConstants.EsfSuppDataUnitCost, It.IsAny<IEnumerable<SupplementaryDataUnitCost>>(), connection, It.IsAny<SqlTransaction>(), cancellationToken)).Returns(Task.CompletedTask);

            await NewService(referenceDataServiceMock.Object, datastoreQueryMock.Object).StoreAsync(connection, It.IsAny<SqlTransaction>(), inputModels, cancellationToken);

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
                Value = 0
            };

            var expectedModel = new SupplementaryDataUnitCost
            {
                ConRefNumber = "ConRefNumber",
                DeliverableCode = "DeliverableCode",
                CalendarYear = 2019,
                CalendarMonth = 1,
                CostType = "CostType",
                ReferenceType = "ReferenceType",
                Reference = "Reference",
                Value = 0
            };

            var referenceDataServiceMock = new Mock<IReferenceDataService>();

            NewService(referenceDataServiceMock.Object).BuildModelFromEntity(model).Should().BeEquivalentTo(expectedModel);
        }

        private StoreESFUnitCost NewService(IReferenceDataService referenceDataService = null, IDataStoreQueryExecutionService dataStoreQueryExecutionService = null)
        {
            return new StoreESFUnitCost(referenceDataService, dataStoreQueryExecutionService, Mock.Of<ILogger>());
        }
    }
}
