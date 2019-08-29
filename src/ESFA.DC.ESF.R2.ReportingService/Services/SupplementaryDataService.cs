using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class SupplementaryDataService : ISupplementaryDataService
    {
        private readonly IEsfRepository _repository;

        private readonly ISourceFileModelMapper _fileModelMapper;

        private readonly ISupplementaryDataModelMapper _supplementaryDataMapper;
        private readonly ILogger _logger;

        private int _startMonth = 8;

        public SupplementaryDataService(
            IEsfRepository repository,
            ISourceFileModelMapper fileModelMapper,
            ISupplementaryDataModelMapper supplementaryDataMapper,
            ILogger logger)
        {
            _repository = repository;
            _fileModelMapper = fileModelMapper;
            _supplementaryDataMapper = supplementaryDataMapper;
            _logger = logger;
        }

        public async Task<IList<SourceFileModel>> GetImportFiles(
            string ukPrn,
            CancellationToken cancellationToken)
        {
            var sourceFiles = new List<SourceFileModel>();

            var contractNumbers = await _repository.GetContractsForProvider(ukPrn, cancellationToken);

            _logger.LogDebug($"Found {contractNumbers.Count} contracts for ukprn {ukPrn}");

            foreach (var contractNumber in contractNumbers)
            {
                _logger.LogDebug($"Getting source file for contract {contractNumber}");

                var file = await _repository.PreviousFiles(ukPrn, contractNumber, cancellationToken);
                if (file == null)
                {
                    continue;
                }

                sourceFiles.Add(_fileModelMapper.GetModelFromEntity(file));
            }

            return sourceFiles;
        }

        public async Task<IDictionary<string, IEnumerable<SupplementaryDataYearlyModel>>> GetSupplementaryData(
            int endYear,
            IEnumerable<SourceFileModel> sourceFiles,
            CancellationToken cancellationToken)
        {
            var supplementaryDataModels = new Dictionary<string, IEnumerable<SupplementaryDataYearlyModel>>();
            foreach (var sourceFile in sourceFiles)
            {
                var supplementaryData =
                    await GetSupplementaryData(
                        endYear,
                        sourceFile.SourceFileId,
                        cancellationToken);

                if (supplementaryData == null)
                {
                    continue;
                }

                supplementaryDataModels.Add(sourceFile.ConRefNumber, supplementaryData);
            }

            return supplementaryDataModels;
        }

        private async Task<IEnumerable<SupplementaryDataYearlyModel>> GetSupplementaryData(
            int endYear,
            int sourceFileId,
            CancellationToken cancellationToken)
        {
            var supplementaryData = await _repository.GetSupplementaryDataPerSourceFile(sourceFileId, cancellationToken);

            return GroupSupplementaryDataIntoYears(endYear, supplementaryData.Select(data => _supplementaryDataMapper.GetModelFromEntity(data)));
        }

        private IEnumerable<SupplementaryDataYearlyModel> GroupSupplementaryDataIntoYears(
            int endYear,
            IEnumerable<SupplementaryDataModel> supplementaryData)
        {
            var yearlySupplementaryData = new List<SupplementaryDataYearlyModel>();

            for (var i = Constants.StartYear; i <= endYear; i++)
            {
                yearlySupplementaryData.Add(new SupplementaryDataYearlyModel
                {
                    FundingYear = i,
                    SupplementaryData = new List<SupplementaryDataModel>()
                });
            }

            var supplementaryDataModels = supplementaryData.ToList();

            if (!supplementaryDataModels.Any())
            {
                return yearlySupplementaryData;
            }

            foreach (var yearlyModel in yearlySupplementaryData)
            {
                yearlyModel.SupplementaryData = supplementaryDataModels
                    .Where(sd => (sd.CalendarYear == yearlyModel.FundingYear && sd.CalendarMonth >= _startMonth)
                                 || (sd.CalendarYear == yearlyModel.FundingYear + 1 && sd.CalendarMonth < _startMonth))
                    .ToList();
            }

            return yearlySupplementaryData;
        }
    }
}