using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;

namespace ESFA.DC.ESF.R2.DataStore
{
    public sealed class StoreClear : IStoreClear
    {
        private readonly IDataStoreQueryExecutionService _dataStoreQueryExecutionService;

        public StoreClear(IDataStoreQueryExecutionService dataStoreQueryExecutionService)
        {
            _dataStoreQueryExecutionService = dataStoreQueryExecutionService;
        }

        public async Task ClearAsync(int ukPrn, string conRefNumber, SqlConnection sqlConnection, SqlTransaction sqlTransaction, CancellationToken cancellationToken)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ukPrn", ukPrn);
            parameters.Add("@conRefNumber", conRefNumber);

            await _dataStoreQueryExecutionService.ExecuteStoredProcedure(
                DataStoreConstants.StoredProcedureNameConstants.DeleteExistingRecords,
                parameters,
                sqlConnection,
                sqlTransaction,
                cancellationToken);
        }
    }
}