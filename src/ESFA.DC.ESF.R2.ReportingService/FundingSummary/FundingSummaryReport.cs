using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Interface;
using ESFA.DC.ExcelService.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary
{
    public sealed class FundingSummaryReport : AbstractReportService, IModelReport
    {
        private const string _excelExtension = ".xlsx";

        private readonly IFundingSummaryReportModelBuilder _modelBuilder;
        private readonly IFundingSummaryReportRenderService _renderService;
        private readonly IExcelFileService _excelFileService;
        private readonly ILogger _logger;

        public FundingSummaryReport(
            IFundingSummaryReportModelBuilder modelBuilder,
            IFundingSummaryReportRenderService renderService,
            IDateTimeProvider dateTimeProvider,
            IExcelFileService excelFileService,
            ILogger logger)
            : base(dateTimeProvider, ReportTaskConstants.TaskGenerateFundingSummaryReport)
        {
            _modelBuilder = modelBuilder;
            _renderService = renderService;
            _excelFileService = excelFileService;
            _logger = logger;
        }

        public string ReportName => ReportNameConstants.FundingSummaryReport;

        public async Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            var fundingSummaryReportModels = await _modelBuilder.Build(esfJobContext, cancellationToken);

            string fileName = GetExternalFilename(esfJobContext.UkPrn, ReportName, esfJobContext.JobId, esfJobContext.SubmissionDateTimeUtc, _excelExtension);

            using (var workbook = _excelFileService.NewWorkbook())
            {
                workbook.Worksheets.Clear();

                foreach (var tab in fundingSummaryReportModels)
                {
                    var worksheet = _excelFileService.GetWorksheetFromWorkbook(workbook, tab.TabName);

                    await _renderService.Render(esfJobContext, tab, worksheet);
                }

                await _excelFileService.SaveWorkbookAsync(workbook, fileName, esfJobContext.BlobContainerName, cancellationToken);
            }

            return fileName;
        }
    }
}