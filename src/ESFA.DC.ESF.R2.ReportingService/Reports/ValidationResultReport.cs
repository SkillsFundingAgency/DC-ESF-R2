using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Jobs.Model;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class ValidationResultReport : AbstractJsonReportService, IValidationResultReport
    {
        private const string _reportExtension = ".json";

        public ValidationResultReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService fileService,
            IJsonSerializationService jsonSerializationService)
            : base(dateTimeProvider, valueProvider, fileService, jsonSerializationService, string.Empty)
        {
            ReportFileName = ReportNameConstants.ValidationResultReport;
        }

        public async Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            var report = GetValidationReport(wrapper.SupplementaryDataModels, wrapper.ValidErrorModels);

            var externalFilename = GetExternalFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue, _reportExtension);

            await SaveJson(esfJobContext, externalFilename, report, cancellationToken);

            return externalFilename;
        }

        private FileValidationResult GetValidationReport(
            ICollection<SupplementaryDataModel> data,
            ICollection<ValidationErrorModel> validationErrors)
        {
            var errors = validationErrors.Where(x => !x.IsWarning).ToList();
            var warnings = validationErrors.Where(x => x.IsWarning).ToList();

            return new FileValidationResult
            {
                TotalLearners = data.GroupBy(w => w.ULN).Count(),
                TotalErrors = errors.Count,
                TotalWarnings = warnings.Count,
                TotalWarningLearners = warnings.GroupBy(w => w.ULN).Count(),
                TotalErrorLearners = errors.GroupBy(e => e.ULN).Count(),
                ErrorMessage = validationErrors.FirstOrDefault(x => string.IsNullOrEmpty(x.ConRefNumber))?.ErrorMessage
            };
        }
    }
}
