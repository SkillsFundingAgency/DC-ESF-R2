using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public sealed class StoreFileDetails : IStoreFileDetails
    {
        private readonly IDataStoreQueryExecutionService _dataStoreQueryExecutionService;

        public StoreFileDetails(IDataStoreQueryExecutionService dataStoreQueryExecutionService)
        {
            _dataStoreQueryExecutionService = dataStoreQueryExecutionService;
        }

        public async Task<int> StoreAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, ISourceFileModel sourceFile, CancellationToken cancellationToken)
        {
            string insertFileDetails =
                    "INSERT INTO [dbo].[SourceFile] ([ConRefNumber], [UKPRN], [Filename], [DateTime], [FilePreparationDate]) " +
                    "VALUES (@ConRefNumber, @UKPRN, @FileName, @SuppliedDate, @PreparationDate); SELECT CAST(SCOPE_IDENTITY() as int)";

            cancellationToken.ThrowIfCancellationRequested();

            var parameters = new DynamicParameters();
            parameters.Add("@ConRefNumber", sourceFile.ConRefNumber);
            parameters.Add("@UKPRN", sourceFile.UKPRN);
            parameters.Add("@FileName", sourceFile.FileName);
            parameters.Add("@SuppliedDate", sourceFile.SuppliedDate?.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("@PreparationDate", sourceFile.PreparationDate.ToString("yyyy-MM-dd HH:mm:ss"));

            return await _dataStoreQueryExecutionService.ExecuteSqlWithParameterAsync<int>(sqlConnection, sqlTransaction, parameters, insertFileDetails, cancellationToken);
        }
    }
}
