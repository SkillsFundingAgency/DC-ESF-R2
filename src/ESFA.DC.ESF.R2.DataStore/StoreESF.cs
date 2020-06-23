using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreESF : IStoreESF
    {
        private readonly IDataStoreQueryExecutionService _dataStoreQueryExecutionService;
        private readonly ILogger _logger;

        public StoreESF(IDataStoreQueryExecutionService dataStoreQueryExecutionService, ILogger logger)
        {
            _dataStoreQueryExecutionService = dataStoreQueryExecutionService;
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

            var suppData = models?.Select(model => BuildModelFromEntity(model, fileId));

            await _dataStoreQueryExecutionService.BulkCopy(DataStoreConstants.TableNameConstants.EsfSuppData, suppData, connection, transaction, cancellationToken);

            _logger.LogInfo("Finished Persisting ESF Supp Data");
        }

        public SupplementaryData BuildModelFromEntity(SupplementaryDataModel model, int fileId)
        {
            return new SupplementaryData
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
            };
        }
    }
}
