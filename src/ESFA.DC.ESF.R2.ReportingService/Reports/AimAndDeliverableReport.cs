using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class AimAndDeliverableReport : AbstractCsvReportService<AimAndDeliverableModel, AimAndDeliverableMapper>, IModelReport
    {
        private const string ReportExtension = ".csv";

        private readonly IAimAndDeliverableService1819 _aimAndDeliverableService1819;
        private readonly IAimAndDeliverableService1920 _aimAndDeliverableService1920;

        public AimAndDeliverableReport(
            IDateTimeProvider dateTimeProvider,
            ICsvFileService csvFileService,
            IAimAndDeliverableService1819 aimAndDeliverableService1819,
            IAimAndDeliverableService1920 aimAndDeliverableService1920)
            : base(dateTimeProvider, csvFileService, ReportTaskConstants.TaskGenerateEsfAimAndDeliverableReport)
        {
            _aimAndDeliverableService1819 = aimAndDeliverableService1819;
            _aimAndDeliverableService1920 = aimAndDeliverableService1920;

            ReportFileName = ReportNameConstants.AimsAndDeliverableReport;
        }

        public async Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            var externalFileName = GetExternalFilename(esfJobContext.UkPrn, esfJobContext.JobId, sourceFile?.SuppliedDate ?? DateTime.MinValue, ReportExtension);

            cancellationToken.ThrowIfCancellationRequested();

            var ukPrn = esfJobContext.UkPrn;
            var reportModels = await GetModels(ukPrn, esfJobContext.CollectionYear, cancellationToken);

            await WriteCsv(esfJobContext, externalFileName, reportModels, cancellationToken);

            return externalFileName;
        }

        private async Task<IEnumerable<AimAndDeliverableModel>> GetModels(int ukPrn, int collectionYear, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<AimAndDeliverableModel> reportData;

            if (collectionYear == 1819)
            {
                reportData = await _aimAndDeliverableService1819.GetAimAndDeliverableModel(ukPrn, cancellationToken);
            }
            else
            {
                reportData = await _aimAndDeliverableService1920.GetAimAndDeliverableModel(ukPrn, cancellationToken);
            }

            return
                reportData
                .OrderBy(x => x.LearnRefNumber)
                .ThenBy(x => x.ConRefNumber)
                .ThenBy(x => x.LearnStartDate)
                .ThenBy(x => x.AimSeqNumber)
                .ThenBy(x => x.Period)
                .ThenBy(x => x.DeliverableCode);
        }
    }
}