using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Builders;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ExcelService.Interface;

namespace ESFA.DC.ESF.R2.Service
{
    public class ServiceController : IServiceController
    {
        private const string LicenseResource = "ESFA.DC.ESF.R2.Service.Resources.Aspose.Cells.lic";

        private readonly ISourceFileModelBuilder _sourceFileModelBuilder;
        private readonly ITaskHelper _taskHelper;
        private readonly IPeriodHelper _periodHelper;
        private readonly IFileValidationService _fileValidationService;
        private readonly IReportingController _reportingController;
        private readonly IStorageController _storageController;
        private readonly IValidationErrorMessageService _validationErrorMessageService;
        private readonly IExcelFileService _excelFileService;

        public ServiceController(
            ISourceFileModelBuilder sourceFileModelBuilder,
            ITaskHelper taskHelper,
            IPeriodHelper periodHelper,
            IFileValidationService fileValidationService,
            IStorageController storageController,
            IReportingController reportingController,
            IValidationErrorMessageService validationErrorMessageService,
            IExcelFileService excelFileService)
        {
            _sourceFileModelBuilder = sourceFileModelBuilder;
            _taskHelper = taskHelper;
            _periodHelper = periodHelper;
            _fileValidationService = fileValidationService;
            _reportingController = reportingController;
            _storageController = storageController;
            _validationErrorMessageService = validationErrorMessageService;
            _excelFileService = excelFileService;
        }

        public async Task RunTasks(
            IEsfJobContext esfJobContext,
            CancellationToken cancellationToken)
        {
            var wrapper = new SupplementaryDataWrapper();
            var sourceFileModel = _sourceFileModelBuilder.BuildDefault(esfJobContext);

            _periodHelper.CacheCurrentPeriod(esfJobContext);

            _excelFileService.ApplyLicense(Assembly.GetExecutingAssembly().GetManifestResourceStream(LicenseResource));

            if (esfJobContext.Tasks.Contains(Constants.ValidationTask))
            {
                sourceFileModel = _sourceFileModelBuilder.Build(esfJobContext);

                wrapper = await _fileValidationService.GetFile(esfJobContext, sourceFileModel, cancellationToken);
                if (!wrapper.ValidErrorModels.Any())
                {
                    await _validationErrorMessageService.PopulateErrorMessages(cancellationToken);
                    wrapper = await _fileValidationService.RunFileValidators(sourceFileModel, wrapper);
                }

                if (wrapper.ValidErrorModels.Any())
                {
                    await _storageController.StoreValidationOnly(sourceFileModel, wrapper, cancellationToken);
                    await _reportingController.FileLevelErrorReport(esfJobContext, wrapper, sourceFileModel, cancellationToken);
                    return;
                }
            }

            await _taskHelper.ExecuteTasks(esfJobContext, sourceFileModel, wrapper, cancellationToken);
            await _reportingController.ProduceReports(esfJobContext, wrapper, sourceFileModel, cancellationToken);
        }
    }
}
