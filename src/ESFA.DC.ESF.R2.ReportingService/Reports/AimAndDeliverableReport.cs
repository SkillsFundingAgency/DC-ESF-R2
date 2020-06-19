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
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class AimAndDeliverableReport : AbstractReportBuilder, IModelReport
    {
        private readonly IFileService _storage;
        private readonly IAimAndDeliverableService1819 _aimAndDeliverableService1819;
        private readonly IAimAndDeliverableService1920 _aimAndDeliverableService1920;

        public AimAndDeliverableReport(
            IDateTimeProvider dateTimeProvider,
            IFileService storage,
            IValueProvider valueProvider,
            IAimAndDeliverableService1819 aimAndDeliverableService1819,
            IAimAndDeliverableService1920 aimAndDeliverableService1920)
            : base(dateTimeProvider, valueProvider, Constants.TaskGenerateEsfAimAndDeliverableReport)
        {
            _storage = storage;
            _aimAndDeliverableService1819 = aimAndDeliverableService1819;
            _aimAndDeliverableService1920 = aimAndDeliverableService1920;

            ReportFileName = "ESF Round 2 Aim and Deliverable Report";
        }

        public async Task GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            ZipArchive archive,
            CancellationToken cancellationToken)
        {
            var externalFileName = GetExternalFilename(esfJobContext.UkPrn.ToString(), esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue);
            var fileName = GetFilename(esfJobContext.UkPrn.ToString(), esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue);

            cancellationToken.ThrowIfCancellationRequested();

            var ukPrn = esfJobContext.UkPrn;
            string csv = await GetCsv(ukPrn, esfJobContext.CollectionYear, cancellationToken);
            if (csv != null)
            {
                using (var stream = await _storage.OpenWriteStreamAsync(
                    $"{externalFileName}.csv",
                    esfJobContext.BlobContainerName,
                    cancellationToken))
                {
                    using (var writer = new StreamWriter(stream, new UTF8Encoding(false, true)))
                    {
                        writer.Write(csv);
                    }
                }

                await WriteZipEntry(archive, $"{fileName}.csv", csv);
            }
        }

        private async Task<string> GetCsv(int ukPrn, int collectionYear, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<AimAndDeliverableModel> reportData;

            if (collectionYear == 1819)
            {
                reportData = (await _aimAndDeliverableService1819.GetAimAndDeliverableModel(ukPrn, cancellationToken)).ToList();
            }
            else
            {
                reportData = (await _aimAndDeliverableService1920.GetAimAndDeliverableModel(ukPrn, cancellationToken)).ToList();
            }

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