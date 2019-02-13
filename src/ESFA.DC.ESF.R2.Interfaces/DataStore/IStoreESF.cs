using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.DataStore
{
    public interface IStoreESF
    {
        Task StoreAsync(SqlConnection connection, SqlTransaction transaction, int FileId, IEnumerable<SupplementaryDataModel> models, CancellationToken cancellationToken);
    }
}