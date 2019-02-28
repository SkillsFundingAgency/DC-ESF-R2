using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.IO.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class AimAndDeliverableReport : AbstractReportBuilder, IModelReport
    {
        private readonly IKeyValuePersistenceService _storage;
        private readonly IAimAndDeliverableService _aimAndDeliverableService;

        public AimAndDeliverableReport(
            IDateTimeProvider dateTimeProvider,
            IStreamableKeyValuePersistenceService storage,
            IValueProvider valueProvider,
            IAimAndDeliverableService aimAndDeliverableService)
            : base(dateTimeProvider, valueProvider)
        {
            _storage = storage;
            _aimAndDeliverableService = aimAndDeliverableService;

            ReportFileName = "ESF Round 2 Aim and Deliverable Report";
        }

        public async Task GenerateReport(
            SupplementaryDataWrapper wrapper,
            SourceFileModel sourceFile,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            var externalFileName = GetExternalFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue);
            var fileName = GetFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue);

            if (cancellationToken.IsCancellationRequested)
            {
                return;
            }

            var ukPrn = Convert.ToInt32(sourceFile.UKPRN);
            string csv = await GetCsv(ukPrn, cancellationToken);
            if (csv != null)
            {
                await _storage.SaveAsync($"{externalFileName}.csv", csv, cancellationToken);
                await WriteZipEntry(archive, $"{fileName}.csv", csv);
            }
        }

        private async Task<string> GetCsv(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var reportData = await _aimAndDeliverableService.GetAimAndDeliverableModel(ukPrn, cancellationToken);

            using (var ms = new MemoryStream())
            {
                var utF8Encoding = new UTF8Encoding(true, true);
                using (var textWriter = new StreamWriter(ms, utF8Encoding))
                {
                    using (var csvWriter = new CsvWriter(textWriter))
                    {
                        WriteCsvRecords<AimAndDeliverableMapper, AimAndDeliverableModel>(csvWriter, reportData);

                        csvWriter.Flush();
                        textWriter.Flush();
                        return Encoding.UTF8.GetString(ms.ToArray());
                    }
                }
            }
        }
    }
}