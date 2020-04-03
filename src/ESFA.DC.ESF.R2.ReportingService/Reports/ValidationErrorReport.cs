using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ReportingService.Comparers;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class ValidationErrorReport : AbstractReportBuilder, IValidationReport
    {
        private readonly IFileService _storage;
        private readonly ValidationComparer _comparer;

        public ValidationErrorReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService storage,
            IValidationComparer comparer)
            : base(dateTimeProvider, valueProvider, string.Empty)
        {
            ReportFileName = "ESF (Round 2) Supplementary Data Rule Violation Report";

            _storage = storage;
            _comparer = comparer as ValidationComparer;
        }

        public async Task GenerateReport(
            JobContextModel jobContextModel,
            SourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            string csv = GetCsv(wrapper.ValidErrorModels.ToList());

            ReportFileName = $"{sourceFile.ConRefNumber} " + ReportFileName;
            string externalFileName = GetExternalFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue);
            string fileName = GetFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue);

            using (var stream = await _storage.OpenWriteStreamAsync(
                $"{externalFileName}.csv",
                jobContextModel.BlobContainerName,
                cancellationToken))
            {
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false, true)))
                {
                    writer.Write(csv);
                }
            }

            await WriteZipEntry(archive, $"{fileName}.csv", csv);
        }

        private string GetCsv(List<ValidationErrorModel> validationErrorModels)
        {
            validationErrorModels.Sort(_comparer);

            using (MemoryStream ms = new MemoryStream())
            {
                UTF8Encoding utF8Encoding = new UTF8Encoding(false, true);
                using (TextWriter textWriter = new StreamWriter(ms, utF8Encoding))
                {
                    using (CsvWriter csvWriter = new CsvWriter(textWriter))
                    {
                        WriteCsvRecords<ValidationErrorMapper, ValidationErrorModel>(csvWriter, validationErrorModels);
                        csvWriter.Flush();
                        textWriter.Flush();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}
