using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreESF : IStoreESF
    {
        private List<SupplementaryData> _supplementaryData;

        public async Task StoreAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            int fileId,
            IEnumerable<SupplementaryDataModel> models,
            CancellationToken cancellationToken)
        {
            _supplementaryData = new List<SupplementaryData>();

            foreach (var model in models)
            {
                _supplementaryData.Add(new SupplementaryData
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
            }

            await SaveData(connection, transaction, cancellationToken);
        }

        private async Task SaveData(SqlConnection connection, SqlTransaction transaction, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using (var bulkInsert = new BulkInsert(connection, transaction, cancellationToken))
            {
                await bulkInsert.Insert("dbo.SupplementaryData", _supplementaryData);
            }
        }
    }
}
