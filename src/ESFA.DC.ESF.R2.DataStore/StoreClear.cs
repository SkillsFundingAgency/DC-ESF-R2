using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;

namespace ESFA.DC.ESF.R2.DataStore
{
    public sealed class StoreClear : IStoreClear
    {
        public async Task ClearAsync(int ukPrn, string conRefNum, SqlConnection sqlConnection, SqlTransaction sqlTransaction, CancellationToken cancellationToken)
        {
            using (SqlCommand sqlCommand = new SqlCommand(DataStoreConstants.StoredProcedureNameConstants.DeleteExistingRecords, sqlConnection, sqlTransaction))
            {
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@ukprn", SqlDbType.Int).Value = ukPrn;
                sqlCommand.Parameters.Add("@conRefNum", SqlDbType.NVarChar).Value = conRefNum;
                sqlCommand.CommandTimeout = 600;
                await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
            }
        }
    }
}