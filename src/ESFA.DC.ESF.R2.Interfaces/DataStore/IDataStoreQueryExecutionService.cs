using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.DataStore
{
    public interface IDataStoreQueryExecutionService
    {
        Task<T> ExecuteSqlWithParameterAsync<T>(SqlConnection connection, SqlTransaction transaction, DynamicParameters parameters, string sql, CancellationToken cancellationToken);

        Task ExecuteStoredProcedure(string sprocName, DynamicParameters parameters, SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken);

        Task BulkCopy<T>(string tableName, IEnumerable<T> data, SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken);
    }
}
