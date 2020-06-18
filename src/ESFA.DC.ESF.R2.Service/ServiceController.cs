using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Service
{
    public class ServiceController : IServiceController
    {
        private readonly IFileHelper _fileHelper;
        private readonly ITaskHelper _taskHelper;
        private readonly IPeriodHelper _periodHelper;
        private readonly IFileValidationService _fileValidationService;
        private readonly IReportingController _reportingController;
        private readonly IStorageController _storageController;
        private readonly IValidationErrorMessageService _validationErrorMessageService;

        public ServiceController(
            IFileHelper fileHelper,
            ITaskHelper taskHelper,
            IPeriodHelper periodHelper,
            IFileValidationService fileValidationService,
            IStorageController storageController,
            IReportingController reportingController,
            IValidationErrorMessageService validationErrorMessageService)
        {
            _fileHelper = fileHelper;
            _taskHelper = taskHelper;
            _periodHelper = periodHelper;
            _fileValidationService = fileValidationService;
            _reportingController = reportingController;
            _storageController = storageController;
            _validationErrorMessageService = validationErrorMessageService;
        }

        public async Task RunTasks(
            IEsfJobContext esfJobContext,
            CancellationToken cancellationToken)
        {
            var wrapper = new SupplementaryDataWrapper();
            var sourceFileModel = new SourceFileModel() { SuppliedDate = esfJobContext.SubmissionDateTimeUtc };

            _periodHelper.CacheCurrentPeriod(esfJobContext);

            if (esfJobContext.Tasks.Contains(Constants.ValidationTask))
            {
                sourceFileModel = _fileHelper.GetSourceFileData(esfJobContext);

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
