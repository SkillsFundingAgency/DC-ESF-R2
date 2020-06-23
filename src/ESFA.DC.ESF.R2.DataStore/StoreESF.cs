using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.BulkCopy.Interfaces;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreESF : IStoreESF
    {
        private readonly IBulkInsert _bulkInsert;
        private readonly ILogger _logger;

        public StoreESF(IBulkInsert bulkInsert, ILogger logger)
        {
            _bulkInsert = bulkInsert;
            _logger = logger;
        }

        public async Task StoreAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            int fileId,
            IEnumerable<SupplementaryDataModel> models,
            CancellationToken cancellationToken)
        {
            _logger.LogInfo("Persisting ESF Supp Data");

            var suppData = models?.Select(model => new SupplementaryData
            {
                ConRefNumber = model.ConRefNumber,
                DeliverableCode = model.DeliverableCode,
                CalendarYear = model.CalendarYear ?? 0,
                CalendarMonth = model.CalendarMonth ?? 0,
                CostType = model.CostType,
                ReferenceType = model.ReferenceType,
                Reference = model.Reference,
                ULN = model.ULN,
                ProviderSpecifiedReference = model.ProviderSpecifiedReference,
                Value = model.Value,
                LearnAimRef = model.LearnAimRef,
                SupplementaryDataPanelDate = model.SupplementaryDataPanelDate,
                SourceFileId = fileId
            });

            await _bulkInsert.Insert(DataStoreConstants.TableNameConstants.EsfSuppData, suppData, connection, transaction, cancellationToken);

            _logger.LogInfo("Finished Persisting ESF Supp Data");
        }
    }
}
