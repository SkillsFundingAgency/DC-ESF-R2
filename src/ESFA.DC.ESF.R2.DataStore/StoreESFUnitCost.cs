using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreESFUnitCost : IStoreESFUnitCost
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IDataStoreQueryExecutionService _dataStoreQueryExecutionService;
        private readonly ILogger _logger;

        public StoreESFUnitCost(IReferenceDataService referenceDataService, IDataStoreQueryExecutionService dataStoreQueryExecutionService, ILogger logger)
        {
            _referenceDataService = referenceDataService;
            _dataStoreQueryExecutionService = dataStoreQueryExecutionService;
            _logger = logger;
        }

        public async Task StoreAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            IEnumerable<SupplementaryDataModel> models,
            CancellationToken cancellationToken)
        {
            _logger.LogInfo("Persisting ESF Supp Data Unit Costs");

            var suppDataUnitCosts = models?
                .Where(x => ESFConstants.UnitCostDeliverableCodes.Any(ucdc => ucdc.CaseInsensitiveEquals(x.DeliverableCode)))
                .Select(BuildModelFromEntity);

            await _dataStoreQueryExecutionService.BulkCopy(DataStoreConstants.TableNameConstants.EsfSuppDataUnitCost, suppDataUnitCosts, connection, transaction, cancellationToken);

            _logger.LogInfo("Finished Persisting ESF Supp Data Unit Costs");
        }

        public SupplementaryDataUnitCost BuildModelFromEntity(SupplementaryDataModel model)
        {
            return new SupplementaryDataUnitCost
            {
                ConRefNumber = model.ConRefNumber,
                DeliverableCode = model.DeliverableCode,
                CalendarYear = model.CalendarYear ?? 0,
                CalendarMonth = model.CalendarMonth ?? 0,
                CostType = model.CostType,
                ReferenceType = model.ReferenceType,
                Reference = model.Reference,
                Value = _referenceDataService.GetDeliverableUnitCostForDeliverableCode(model.ConRefNumber, model.DeliverableCode) ?? 0
            };
        }
    }
}
