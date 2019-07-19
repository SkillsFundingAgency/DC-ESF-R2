using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Service.Config;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StorageController : IStorageController
    {
        private readonly IStoreESF _store;
        private readonly IStoreESFUnitCost _storeEsfUnitCost;
        private readonly IStoreFileDetails _storeFileDetails;
        private readonly IStoreValidation _storeValidation;
        private readonly ESFConfiguration _dbConfiguration;
        private readonly ILogger _logger;

        public StorageController(
            ESFConfiguration databaseConfiguration,
            IStoreESF store,
            IStoreESFUnitCost storeEsfUnitCost,
            IStoreValidation storeValidation,
            IStoreFileDetails storeFileDetails,
            ILogger logger)
        {
            _dbConfiguration = databaseConfiguration;
            _store = store;
            _storeEsfUnitCost = storeEsfUnitCost;
            _storeFileDetails = storeFileDetails;
            _storeValidation = storeValidation;
            _logger = logger;
        }

        public async Task<bool> StoreData(
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            bool successfullyCommitted = false;

            using (SqlConnection connection =
                new SqlConnection(_dbConfiguration.ESFR2ConnectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();

                    transaction = connection.BeginTransaction();

                    var ukPrn = Convert.ToInt32(sourceFile.UKPRN);

                    var storeClear = new StoreClear(connection, transaction);
                    await storeClear.ClearAsync(ukPrn, sourceFile.ConRefNumber, cancellationToken);

                    int fileId = await _storeFileDetails.StoreAsync(connection, transaction, cancellationToken, sourceFile);

                    await _storeValidation.StoreAsync(connection, transaction, fileId, wrapper.ValidErrorModels, cancellationToken);

                    _logger.LogDebug($"{wrapper.SupplementaryDataModels.Count} suppData rows to save.");
                    await _store.StoreAsync(connection, transaction, fileId, wrapper.SupplementaryDataModels, cancellationToken);

                    await _storeEsfUnitCost.StoreAsync(connection, transaction, wrapper.SupplementaryDataModels, cancellationToken);

                    transaction.Commit();
                    successfullyCommitted = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to persist to DEDs", ex);
                    throw;
                }
                finally
                {
                    if (!successfullyCommitted)
                    {
                        try
                        {
                            transaction?.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            _logger.LogError("Failed to rollback DEDs persist transaction", ex2);
                            throw;
                        }
                    }
                }
            }

            return successfullyCommitted;
        }

        public async Task<bool> StoreValidationOnly(
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            bool successfullyCommitted = false;

            using (SqlConnection connection =
                new SqlConnection(_dbConfiguration.ESFR2ConnectionString))
            {
                SqlTransaction transaction = null;
                try
                {
                    await connection.OpenAsync(cancellationToken);

                    cancellationToken.ThrowIfCancellationRequested();

                    transaction = connection.BeginTransaction();

                    int fileId = await _storeFileDetails.StoreAsync(connection, transaction, cancellationToken, sourceFile);

                    await _storeValidation.StoreAsync(connection, transaction, fileId, wrapper.ValidErrorModels, cancellationToken);

                    transaction.Commit();
                    successfullyCommitted = true;
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to persist to DEDs", ex);
                    throw;
                }
                finally
                {
                    if (!successfullyCommitted)
                    {
                        try
                        {
                            transaction?.Rollback();
                        }
                        catch (Exception ex2)
                        {
                            _logger.LogError("Failed to rollback DEDs persist transaction", ex2);
                            throw;
                        }
                    }
                }
            }

            return successfullyCommitted;
        }
    }
}
