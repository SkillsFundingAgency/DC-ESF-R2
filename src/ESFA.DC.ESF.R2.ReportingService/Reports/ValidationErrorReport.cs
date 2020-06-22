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
using ESFA.DC.ESF.R2.ReportingService.Comparers;
using ESFA.DC.ESF.R2.ReportingService.Constants;
using ESFA.DC.ESF.R2.ReportingService.Mappers;
using ESFA.DC.FileService.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.Reports
{
    public class ValidationErrorReport : AbstractCsvReportService<ValidationErrorModel, ValidationErrorMapper>, IValidationReport
    {
        private const string _reportExtension = ".csv";
        private readonly ValidationComparer _comparer;

        public ValidationErrorReport(
            IDateTimeProvider dateTimeProvider,
            IValueProvider valueProvider,
            IFileService fileService,
            ICsvFileService csvFileService,
            IValidationComparer comparer)
            : base(dateTimeProvider, valueProvider, fileService, csvFileService, string.Empty)
        {
            ReportFileName = ReportNameConstants.ValidationErrorReport;

            _comparer = comparer as ValidationComparer;
        }

        public async Task<string> GenerateReport(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFile,
            SupplementaryDataWrapper wrapper,
            CancellationToken cancellationToken)
        {
            ReportFileName = $"{sourceFile.ConRefNumber} " + ReportFileName;

            string externalFileName = GetExternalFilename(sourceFile.UKPRN, sourceFile.JobId ?? 0, sourceFile.SuppliedDate ?? DateTime.MinValue, _reportExtension);

            var validationErrorModels = wrapper.ValidErrorModels.ToList();

            validationErrorModels.Sort(_comparer);

            await WriteCsv(esfJobContext, externalFileName, validationErrorModels, cancellationToken);

            return externalFileName;
        }
    }
}
