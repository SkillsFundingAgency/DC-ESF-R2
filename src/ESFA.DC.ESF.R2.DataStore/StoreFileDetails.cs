﻿using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataStore
{
    public sealed class StoreFileDetails : IStoreFileDetails
    {
        private SqlConnection _sqlConnection;

        private SqlTransaction _sqlTransaction;

        public async Task<int> StoreAsync(SqlConnection sqlConnection, SqlTransaction sqlTransaction, CancellationToken cancellationToken, SourceFileModel sourceFile)
        {
            _sqlConnection = sqlConnection;
            _sqlTransaction = sqlTransaction;

            return await StoreAsync(sourceFile, cancellationToken);
        }

        private async Task<int> StoreAsync(
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            string insertFileDetails =
                    "INSERT INTO [dbo].[SourceFile] ([ConRefNumber], [UKPRN], [Filename], [DateTime], [FilePreparationDate]) output INSERTED.SourceFileId VALUES ('" +
                    $"{sourceFile.ConRefNumber}', '{sourceFile.UKPRN}', '{sourceFile.FileName}', " +
                    $"'{sourceFile.SuppliedDate?.ToString("yyyy-MM-dd HH:mm:ss")}', '{sourceFile.PreparationDate:yyyy-MM-dd HH:mm:ss}')";

            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }

            using (var sqlCommand =
                new SqlCommand(insertFileDetails, _sqlConnection, _sqlTransaction))
            {
                return (int)await sqlCommand.ExecuteScalarAsync(cancellationToken);
            }
        }
    }
}