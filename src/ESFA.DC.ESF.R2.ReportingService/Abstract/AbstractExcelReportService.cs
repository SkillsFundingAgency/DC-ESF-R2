using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ExcelService.Interface;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Abstract
{
    public abstract class AbstractExcelReportService : AbstractReportService
    {
        private const string _defaultWorkSheetName = "FundingSummaryReport";
        private readonly IExcelFileService _excelFileService;

        protected AbstractExcelReportService(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService fileService,
            IExcelFileService excelFileService,
            string taskName)
             : base(dateTimeProvider, valueProvider, fileService, taskName)
        {
            _excelFileService = excelFileService;
        }

        public async Task WriteExcelFile(IEsfJobContext esfJobContext, string fileName, Workbook workbook, CancellationToken cancellationToken)
        {
            await _excelFileService.SaveWorkbookAsync(workbook, fileName, esfJobContext.BlobContainerName, cancellationToken);
        }
    }
}