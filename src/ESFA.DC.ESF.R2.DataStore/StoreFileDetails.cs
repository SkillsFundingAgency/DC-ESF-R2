using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public sealed class StoreFileDetails : IStoreFileDetails
    {
        private SqlConnection _sqlConnection;

        private SqlTransaction _sqlTransaction;

        public async Task<int> StoreAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, CancellationToken cancellationToken, ISourceFileModel sourceFile)
        {
            _sqlConnection = sqlConnection;
            _sqlTransaction = sqlTransaction;

            return await StoreAsync(sourceFile, cancellationToken);
        }

        private async Task<int> StoreAsync(
            ISourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            string insertFileDetails =
                    "INSERT INTO [dbo].[SourceFile] ([ConRefNumber], [UKPRN], [Filename], [DateTime], [FilePreparationDate]) output INSERTED.SourceFileId VALUES ('" +
                    $"{sourceFile.ConRefNumber}', '{sourceFile.UKPRN}', '{sourceFile.FileName}', " +
                    $"'{sourceFile.SuppliedDate?.ToString("yyyy-MM-dd HH:mm:ss")}', '{sourceFile.PreparationDate:yyyy-MM-dd HH:mm:ss}')";

            cancellationToken.ThrowIfCancellationRequested();

            using (var sqlCommand =
                new SqlCommand(insertFileDetails, _sqlConnection, _sqlTransaction))
            {
                return (int)await sqlCommand.ExecuteScalarAsync(cancellationToken);
            }
        }
    }
}
