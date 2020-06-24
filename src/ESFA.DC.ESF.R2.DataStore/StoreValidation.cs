using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Database.EF;
using ESFA.DC.ESF.R2.DataStore.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataStore;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.DataStore
{
    public class StoreValidation : IStoreValidation
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IDataStoreQueryExecutionService _dataStoreQueryExecutionService;
        private readonly ILogger _logger;

        public StoreValidation(IDateTimeProvider dateTimeProvider, IDataStoreQueryExecutionService dataStoreQueryExecutionService, ILogger logger)
        {
            _dateTimeProvider = dateTimeProvider;
            _dataStoreQueryExecutionService = dataStoreQueryExecutionService;
            _logger = logger;
        }

        public async Task StoreAsync(
            SqlConnection connection,
            SqlTransaction transaction,
            int fileId,
            IEnumerable<ValidationErrorModel> models,
            CancellationToken cancellationToken)
        {
            _logger.LogInfo("Persisting ESF Supp Data Validation Errors");

            var createdOn = _dateTimeProvider.GetNowUtc();

            var validationErrors = models?.Select(model => BuildModelFromEntity(model, createdOn, fileId));

            await _dataStoreQueryExecutionService.BulkCopy(DataStoreConstants.TableNameConstants.EsfSuppDataValidationError, validationErrors, connection, transaction, cancellationToken);

            _logger.LogInfo("Finished Persisting ESF Supp Data Validation Errors");
        }

        public ValidationError BuildModelFromEntity(ValidationErrorModel model, DateTime createdOn, int fileId)
        {
            return new ValidationError
            {
                Severity = model.IsWarning ? DataStoreConstants.ErrorSeverity.Warning : DataStoreConstants.ErrorSeverity.Error,
                RuleId = model.RuleName,
                ErrorMessage = model.ErrorMessage,
                CreatedOn = createdOn,
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
            };
        }
    }
}
