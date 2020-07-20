using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.BulkCopy.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.DataStore;

namespace ESFA.DC.ESF.R2.DataStore.Service
{
    public class DataStoreQueryExecutionService : IDataStoreQueryExecutionService
    {
        private readonly IBulkInsert _bulkInsert;

        public DataStoreQueryExecutionService(IBulkInsert bulkInsert)
        {
            _bulkInsert = bulkInsert;
        }

        public async Task<T> ExecuteSqlWithParameterAsync<T>(SqlConnection connection, SqlTransaction transaction, DynamicParameters parameters, string sql, CancellationToken cancellationToken)
        {
            var commandDefinition = new CommandDefinition(sql, parameters, commandTimeout: 600, transaction: transaction, cancellationToken: cancellationToken);

            return await connection.QuerySingleAsync<T>(commandDefinition);
        }

        public async Task ExecuteStoredProcedure(string sprocName, DynamicParameters parameters, SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken)
        {
            var commandDefinition = new CommandDefinition(sprocName, parameters, commandType: CommandType.StoredProcedure, transaction: transaction, commandTimeout: 600, cancellationToken: cancellationToken);

            await connection.QueryAsync(commandDefinition);
        }

        public async Task BulkCopy<T>(string tableName, IEnumerable<T> data, SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken)
        {
            await _bulkInsert.Insert(tableName, data, connection, transaction, cancellationToken);
        }
    }
}
