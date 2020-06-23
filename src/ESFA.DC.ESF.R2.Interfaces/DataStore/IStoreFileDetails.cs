using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.Interfaces.DataStore
{
    public interface IStoreFileDetails
    {
        Task<int> StoreAsync(SqlConnection sqlConnection, ISourceFileModel sourceFile, CancellationToken cancellationToken);
    }
}