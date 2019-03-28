using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class SupplementaryDataService : ISupplementaryDataService
    {
        private readonly IEsfRepository _repository;

        private readonly ISourceFileModelMapper _fileModelMapper;

        private readonly ISupplementaryDataModelMapper _supplementaryDataMapper;

        private int _startYear = 2019;
        private int _endYear = 2022;
        private int _startMonth = 8;

        public SupplementaryDataService(
            IEsfRepository repository,
            ISourceFileModelMapper fileModelMapper,
            ISupplementaryDataModelMapper supplementaryDataMapper)
        {
            _repository = repository;
            _fileModelMapper = fileModelMapper;
            _supplementaryDataMapper = supplementaryDataMapper;
        }

        public async Task<IList<SourceFileModel>> GetImportFiles(
            string ukPrn,
            CancellationToken cancellationToken)
        {
            var sourceFiles = new List<SourceFileModel>();

            var contractNumbers = await _repository.GetContractsForProvider(ukPrn, cancellationToken);

            foreach (var contractNumber in contractNumbers)
            {
                var file = await _repository.PreviousFiles(ukPrn, contractNumber, cancellationToken);
                if (file == null)
                {
                    continue;
                }

                sourceFiles.Add(_fileModelMapper.GetModelFromEntity(file));
            }

            return sourceFiles;
        }

        public async Task<IDictionary<int, IEnumerable<SupplementaryDataYearlyModel>>> GetSupplementaryData(
            IEnumerable<SourceFileModel> sourceFiles,
            CancellationToken cancellationToken)
        {
            var supplementaryDataModels = new Dictionary<int, IEnumerable<SupplementaryDataYearlyModel>>();
            foreach (var sourceFile in sourceFiles)
            {
                var supplementaryData =
                    await GetSupplementaryData(
                        sourceFile.SourceFileId,
                        cancellationToken);

                if (supplementaryData == null)
                {
                    continue;
                }

                supplementaryDataModels.Add(sourceFile.SourceFileId, supplementaryData);
            }

            return supplementaryDataModels;
        }

        private async Task<IEnumerable<SupplementaryDataYearlyModel>> GetSupplementaryData(
            int sourceFileId,
            CancellationToken cancellationToken)
        {
            var supplementaryData = await _repository.GetSupplementaryDataPerSourceFile(sourceFileId, cancellationToken);

            return GroupSupplementaryDataIntoYears(supplementaryData.Select(data => _supplementaryDataMapper.GetModelFromEntity(data)));
        }

        private IEnumerable<SupplementaryDataYearlyModel> GroupSupplementaryDataIntoYears(IEnumerable<SupplementaryDataModel> supplementaryData)
        {
            var yearlySupplementaryData = new List<SupplementaryDataYearlyModel>();

            for (var i = _startYear; i < _endYear; i++)
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