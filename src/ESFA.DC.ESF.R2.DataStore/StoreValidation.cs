using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreValidation : IStoreValidation
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private List<ValidationError> _validationData;

        public StoreValidation(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task StoreAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            int fileId,
            IEnumerable<ValidationErrorModel> models,
            CancellationToken cancellationToken)
        {
            _validationData = new List<ValidationError>();

            foreach (var model in models)
            {
                _validationData.Add(new ValidationError
                {
                    Severity = model.IsWarning ? "W" : "E",
                    RuleId = model.RuleName,
                    ErrorMessage = model.ErrorMessage,
                    CreatedOn = _dateTimeProvider.GetNowUtc(),
                    ConRefNumber = model.ConRefNumber,
                    DeliverableCode = model.DeliverableCode,
                    CalendarYear = model.CalendarYear,
                    CalendarMonth = model.CalendarMonth,
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
                await bulkInsert.Insert("dbo.ValidationError", _validationData);
            }
        }
    }
}
