using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.Abstract;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class ValidationErrorReport : AbstractCsvReportService<ValidationErrorModel, ValidationErrorMapper>, IValidationReport
    {
        private const string _reportExtension = ".csv";

        public ValidationErrorReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService fileService,
            ICsvFileService csvFileService)
            : base(dateTimeProvider, valueProvider, fileService, csvFileService, string.Empty)
        {
            ReportFileName = ReportNameConstants.ValidationErrorReport;
        }

        public async Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            ReportFileName = $"{sourceFile.ConRefNumber} " + ReportFileName;

            string externalFileName = GetExternalFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue, _reportExtension);

            await WriteCsv(esfJobContext, externalFileName, wrapper.ValidErrorModels.OrderBy(s => s.IsWarning).ThenBy(s => s.RuleName), cancellationToken);

            return externalFileName;
        }
    }
}
