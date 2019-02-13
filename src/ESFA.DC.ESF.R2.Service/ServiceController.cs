using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Controllers;
using ESFA.DC.ESF.R2.Interfaces.Helpers;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.JobContextManager.Model.Interface;

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

        public ServiceController(
            IFileHelper fileHelper,
            ITaskHelper taskHelper,
            IPeriodHelper periodHelper,
            IFileValidationService fileValidationService,
            IStorageController storageController,
            IReportingController reportingController)
        {
            _fileHelper = fileHelper;
            _taskHelper = taskHelper;
            _periodHelper = periodHelper;
            _fileValidationService = fileValidationService;
            _reportingController = reportingController;
            _storageController = storageController;
        }

        public async Task RunTasks(
            IJobContextMessage jobContextMessage,
            IReadOnlyList<ITaskItem> tasks,
            CancellationToken cancellationToken)
        {
            var wrapper = new SupplementaryDataWrapper();
            var sourceFileModel = new SourceFileModel();

            _periodHelper.CacheCurrentPeriod(jobContextMessage);

            if (tasks.SelectMany(t => t.Tasks).Contains(Constants.ValidationTask))
            {
                sourceFileModel = _fileHelper.GetSourceFileData(jobContextMessage);

                wrapper = await _fileValidationService.GetFile(sourceFileModel, cancellationToken);
                if (!wrapper.ValidErrorModels.Any())
                {
                    wrapper = _fileValidationService.RunFileValidators(sourceFileModel, wrapper);
                }

                if (wrapper.ValidErrorModels.Any())
                {
                    await _storageController.StoreValidationOnly(sourceFileModel, wrapper, cancellationToken);
                    await _reportingController.FileLevelErrorReport(wrapper, sourceFileModel, cancellationToken);
                    return;
                }
            }

            await _taskHelper.ExecuteTasks(tasks, sourceFileModel, wrapper, cancellationToken);
        }
    }
}
