using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreESFUnitCost : IStoreESFUnitCost
    {
        private readonly IReferenceDataService _referenceDataService;

        private List<SupplementaryDataUnitCost> _supplementaryUnitCosts;

        public StoreESFUnitCost(IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
        }

        public async Task StoreAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            IEnumerable<SupplementaryDataModel> models,
            CancellationToken cancellationToken)
        {
            _supplementaryUnitCosts = new List<SupplementaryDataUnitCost>();

            foreach (var model in models)
            {
                if (!ESFConstants.UnitCostDeliverableCodes
                    .Any(ucdc => ucdc.CaseInsensitiveEquals(model.DeliverableCode)))
                {
                    continue;
                }

                var unitCost = _referenceDataService
                    .GetDeliverableUnitCosts(model.ConRefNumber, new List<string> { model.DeliverableCode }).ToList();

                _supplementaryUnitCosts.Add(new SupplementaryDataUnitCost
                {
                    ConRefNumber = model.ConRefNumber,
                    DeliverableCode = model.DeliverableCode,
                    CalendarYear = model.CalendarYear ?? 0,
                    CalendarMonth = model.CalendarMonth ?? 0,
                    CostType = model.CostType,
                    ReferenceType = model.ReferenceType,
                    Reference = model.Reference,
                    Value = unitCost.FirstOrDefault()?.UnitCost ?? 0
                });
            }

            await SaveData(connection, transaction, cancellationToken);
        }

        private async Task SaveData(SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var bulkInsert = new BulkInsert(connection, transaction, cancellationToken))
            {
                await bulkInsert.Insert("dbo.SupplementaryDataUnitCost", _supplementaryUnitCosts);
            }
        }
    }
}
