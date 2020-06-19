using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using ESFA.DC.CsvService.Interface;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Service.Mappers;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Services
{
    public class ESFProviderService : IESFProviderService
    {
        private readonly ILogger _logger;
        private readonly ICsvFileService _csvFileService;

        public ESFProviderService(ILogger logger, ICsvFileService csvFileService)
        {
            _logger = logger;
            _csvFileService = csvFileService;
        }

        public async Task<ICollection<SupplementaryDataLooseModel>> GetESFRecordsFromFile(IEsfJobContext esfJobContext, CancellationToken cancellationToken)
        {
            List<SupplementaryDataLooseModel> model = null;

            _logger.LogInfo("Try and get csv from Azure blob.");

            cancellationToken.ThrowIfCancellationRequested();

            var csvConfig = GetCsvConfig();

            return await _csvFileService.ReadAllAsync<SupplementaryDataLooseModel, ESFMapper>(esfJobContext.FileName, esfJobContext.BlobContainerName, cancellationToken, csvConfig);
        }

        private Configuration GetCsvConfig()
        {
            var csvConfig = _csvFileService.BuildDefaultConfiguration();
            csvConfig.TrimOptions = TrimOptions.Trim;
            csvConfig.MissingFieldFound = null;

            return csvConfig;
        }
    }
}
