using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Interfaces;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Services
{
    public class FileValidationService : IFileValidationService
    {
        private readonly IList<IFileLevelValidator> _validators;
        private readonly IESFProviderService _eSFProviderService;
        private readonly ILogger _logger;

        public FileValidationService(IList<IFileLevelValidator> validators, IESFProviderService eSFProviderService, ILogger logger)
        {
            _eSFProviderService = eSFProviderService;
            _validators = validators;
            _logger = logger;
        }

        public async Task<SupplementaryDataWrapper> GetFile(
            IEsfJobContext esfJobContext,
            ISourceFileModel sourceFileModel,
            CancellationToken cancellationToken)
        {
            SupplementaryDataWrapper wrapper = new SupplementaryDataWrapper();
            ICollection<SupplementaryDataLooseModel> esfRecords = new List<SupplementaryDataLooseModel>();
            IList<ValidationErrorModel> errors = new List<ValidationErrorModel>();
            try
            {
                esfRecords = await _eSFProviderService.GetESFRecordsFromFile(esfJobContext, cancellationToken);
                if (esfRecords == null || !esfRecords.Any())
                {
                    _logger.LogInfo("No ESF records to process");
                }
            }
            catch (ValidationException ex)
            {
                _logger.LogError($"The file format is incorrect, key: {sourceFileModel.FileName}", ex);

                errors.Add(new ValidationErrorModel
                {
                    RuleName = "Fileformat_01",
                    ErrorMessage = "The file format is incorrect. Please check the field headers are as per the Guidance document.",
                    IsWarning = false
                });
            }

            wrapper.SupplementaryDataLooseModels = esfRecords;
            wrapper.ValidErrorModels = errors;
            return wrapper;
        }

        public async Task<SupplementaryDataWrapper> RunFileValidators(
            ISourceFileModel sourceFileModel,
            SupplementaryDataWrapper wrapper)
        {
            foreach (var model in wrapper.SupplementaryDataLooseModels)
            {
                foreach (var validator in _validators)
                {
                    if (await validator.IsValid(sourceFileModel, model))
                    {
                        continue;
                    }

                    wrapper.ValidErrorModels.Add(new ValidationErrorModel
                    {
                        RuleName = validator.ErrorName,
                        ErrorMessage = validator.ErrorMessage,
                        IsWarning = false
                    });
                    return wrapper;
                }
            }

            return wrapper;
        }
    }
}
