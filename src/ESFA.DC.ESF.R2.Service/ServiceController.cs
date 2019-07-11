using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        private readonly IIlrReferenceDataCacheService _referenceDataCacheService;
        private readonly IValidationErrorMessageService _validationErrorMessageService;

        public ServiceController(
            IFileHelper fileHelper,
            ITaskHelper taskHelper,
            IPeriodHelper periodHelper,
            IFileValidationService fileValidationService,
            IStorageController storageController,
            IReportingController reportingController,
            IIlrReferenceDataCacheService referenceDataCacheService,
            IValidationErrorMessageService validationErrorMessageService)
        {
            _fileHelper = fileHelper;
            _taskHelper = taskHelper;
            _periodHelper = periodHelper;
            _fileValidationService = fileValidationService;
            _reportingController = reportingController;
            _storageController = storageController;
            _referenceDataCacheService = referenceDataCacheService;
            _validationErrorMessageService = validationErrorMessageService;
        }

        public async Task RunTasks(
            JobContextModel jobContextModel,
            CancellationToken cancellationToken)
        {
            var wrapper = new SupplementaryDataWrapper();
            var sourceFileModel = new SourceFileModel();

            _periodHelper.CacheCurrentPeriod(jobContextModel);

            if (jobContextModel.Tasks.Contains(Constants.ValidationTask))
            {
                sourceFileModel = _fileHelper.GetSourceFileData(jobContextModel);

                wrapper = await _fileValidationService.GetFile(jobContextModel, sourceFileModel, cancellationToken);
                if (!wrapper.ValidErrorModels.Any())
                {
                    await _validationErrorMessageService.PopulateErrorMessages(cancellationToken);
                    wrapper = await _fileValidationService.RunFileValidators(sourceFileModel, wrapper);
                }

                if (wrapper.ValidErrorModels.Any())
                {
                    await _storageController.StoreValidationOnly(sourceFileModel, wrapper, cancellationToken);
                    await _reportingController.FileLevelErrorReport(jobContextModel, wrapper, sourceFileModel, cancellationToken);
                    return;
                }
            }
            else
            {
                await _referenceDataCacheService.PopulateCacheFromJson(jobContextModel, cancellationToken);
            }

            await _taskHelper.ExecuteTasks(jobContextModel, sourceFileModel, wrapper, cancellationToken);
        }
    }
}
