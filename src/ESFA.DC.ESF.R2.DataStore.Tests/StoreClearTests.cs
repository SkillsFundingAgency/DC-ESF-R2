using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.DataStore.Tests
{
    public class StoreClearTests
    {
        [Fact]
        public async Task StoreAsync()
        {
            var cancellationToken = CancellationToken.None;
            var connection = new SqlConnection();

            var sourceFile = new Mock<ISourceFileModel>();

            var ukprn = 1000;
            var conRef = "ConRefNumber";

            var datastoreQueryMock = new Mock<IDataStoreQueryExecutionService>();
            datastoreQueryMock.Setup(x => x.ExecuteStoredProcedure(DataStoreConstants.StoredProcedureNameConstants.DeleteExistingRecords, It.IsAny<DynamicParameters>(), connection, It.IsAny<SqlTransaction>(), cancellationToken)).Returns(Task.CompletedTask);

            await NewService(datastoreQueryMock.Object).ClearAsync(ukprn, conRef, connection, It.IsAny<SqlTransaction>(), cancellationToken);

            datastoreQueryMock.VerifyAll();
        }

        private StoreClear NewService(IDataStoreQueryExecutionService dataStoreQueryExecutionService)
        {
            return new StoreClear(dataStoreQueryExecutionService);
        }
    }
}
