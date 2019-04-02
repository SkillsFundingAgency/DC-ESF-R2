using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Service.Mappers;
using ESFA.DC.FileService.Interface;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Services
{
    public class ESFProviderService : IESFProviderService
    {
        private readonly ILogger _logger;

        private readonly IFileService _storage;

        private readonly SemaphoreSlim _getESFLock;

        public ESFProviderService(
            ILogger logger,
            IFileService storage)
        {
            _logger = logger;
            _storage = storage;
            _getESFLock = new SemaphoreSlim(1, 1);
        }

        public async Task<IList<SupplementaryDataLooseModel>> GetESFRecordsFromFile(
            JobContextModel jobContextMessage,
            SourceFileModel sourceFile,
            CancellationToken cancellationToken)
        {
            List<SupplementaryDataLooseModel> model = null;

            await _getESFLock.WaitAsync(cancellationToken);

            _logger.LogInfo("Try and get csv from Azure blob.");

            cancellationToken.ThrowIfCancellationRequested();

            using (var stream = await _storage.OpenReadStreamAsync(sourceFile.FileName, jobContextMessage.BlobContainerName, cancellationToken))
            {
                using (var reader = new StreamReader(stream))
                {
                    using (var csvReader = new CsvReader(reader))
                    {
                        csvReader.Configuration.RegisterClassMap(new ESFMapper());
                        csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                        model = csvReader.GetRecords<SupplementaryDataLooseModel>().ToList();
                    }
                }
            }

            _getESFLock.Release();

            return model;
        }
    }
}
