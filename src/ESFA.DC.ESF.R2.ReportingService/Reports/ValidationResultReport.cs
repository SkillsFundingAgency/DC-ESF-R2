using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Jobs.Model;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class ValidationResultReport : AbstractReportBuilder, IValidationResultReport
    {
        private readonly IFileService _storage;
        private readonly IJsonSerializationService _jsonSerializationService;

        public ValidationResultReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IJsonSerializationService jsonSerializationService,
            IFileService storage)
            : base(dateTimeProvider, valueProvider, string.Empty)
        {
            ReportFileName = "ESF Supplementary Data Rule Violation Report";

            _jsonSerializationService = jsonSerializationService;
            _storage = storage;
        }

        public async Task GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            var report = GetValidationReport(wrapper.SupplementaryDataModels, wrapper.ValidErrorModels);

            var fileName = GetFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue);
            var externalFilename = GetExternalFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue);

            var json = _jsonSerializationService.Serialize(report);
            await SaveJson(esfJobContext, externalFilename, json, cancellationToken);
            await WriteZipEntry(archive, $"{fileName}.json", json);
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

        private async Task SaveJson(IEsfJobContext esfJobContext, string fileName, string json, CancellationToken cancellationToken)
        {
            using (var stream = await _storage.OpenWriteStreamAsync(
                $"{fileName}.json",
                esfJobContext.BlobContainerName,
                cancellationToken))
            {
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false, true)))
                {
                    writer.Write(json);
                }
            }
        }
    }
}
