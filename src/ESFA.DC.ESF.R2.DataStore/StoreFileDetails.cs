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
        public async Task<int> StoreAsync(SqlConnection sqlConnection, ISourceFileModel sourceFile, CancellationToken cancellationToken)
        {
            string insertFileDetails =
                    "INSERT INTO [dbo].[SourceFile] ([ConRefNumber], [UKPRN], [Filename], [DateTime], [FilePreparationDate]) " +
                    "output INSERTED.SourceFileId VALUES (@ConRefNumber @UKPRN, @FileName, @SuppliedDate, @PreparationDate)";

            cancellationToken.ThrowIfCancellationRequested();

            var parameters = new object[]
            {
                sourceFile.ConRefNumber,
                sourceFile.UKPRN,
                sourceFile.FileName,
                sourceFile.SuppliedDate?.ToString("yyyy-MM-dd HH:mm:ss"),
                sourceFile.PreparationDate.ToString("yyyy-MM-dd HH:mm:ss")
            };

            return await ExecuteSqlWithParameterAsync(sqlConnection, parameters, insertFileDetails, cancellationToken);
        }

        private async Task<int> ExecuteSqlWithParameterAsync(SqlConnection connection, object[] parameters, string sql, CancellationToken cancellationToken)
        {
            var commandDefinition = new Dapper.CommandDefinition(sql, parameters, commandTimeout: 600, cancellationToken: cancellationToken);

            return await connection.QuerySingleAsync(commandDefinition);
        }
    }
}
