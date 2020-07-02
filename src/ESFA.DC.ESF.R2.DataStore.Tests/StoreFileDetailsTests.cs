using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models.Interfaces;
using Moq;
using Xunit;

namespace ESFA.DC.ESF.R2.DataStore.Tests
{
    public class StoreFileDetailsTests
    {
        [Fact]
        public async Task StoreAsync()
        {
            var cancellationToken = CancellationToken.None;
            var connection = new SqlConnection();
            var sourceFile = new Mock<ISourceFileModel>();

            sourceFile.Setup(x => x.ConRefNumber).Returns("ConRef");
            sourceFile.Setup(x => x.UKPRN).Returns("200");
            sourceFile.Setup(x => x.FileName).Returns("FileName");
            sourceFile.Setup(x => x.SuppliedDate).Returns(new DateTime(2019, 8, 1));
            sourceFile.Setup(x => x.PreparationDate).Returns(new DateTime(2019, 8, 1));

            var datastoreQueryMock = new Mock<IDataStoreQueryExecutionService>();
            datastoreQueryMock.Setup(x => x.ExecuteSqlWithParameterAsync<int>(It.IsAny<SqlConnection>(), It.IsAny<SqlTransaction>(), It.IsAny<DynamicParameters>(), It.IsAny<string>(), cancellationToken)).ReturnsAsync(1);

            var sourceFileId = await NewService(datastoreQueryMock.Object).StoreAsync(connection, It.IsAny<SqlTransaction>(), sourceFile.Object, cancellationToken);
            Assert.Equal(1, sourceFileId);

            datastoreQueryMock.VerifyAll();
        }

        private StoreFileDetails NewService(IDataStoreQueryExecutionService dataStoreQueryExecutionService)
        {
            return new StoreFileDetails(dataStoreQueryExecutionService);
        }
    }
}
