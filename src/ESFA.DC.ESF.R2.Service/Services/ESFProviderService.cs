using System;
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
using ESFA.DC.IO.Interfaces;
using ESFA.DC.Logging.Interfaces;

namespace ESFA.DC.ESF.R2.Service.Services
{
    public class ESFProviderService : IESFProviderService
    {
        private readonly ILogger _logger;

        private readonly IStreamableKeyValuePersistenceService _storage;

        private readonly SemaphoreSlim _getESFLock;

        public ESFProviderService(
            ILogger logger,
            IStreamableKeyValuePersistenceService storage)
        {
            _logger = logger;
            _storage = storage;
            _getESFLock = new SemaphoreSlim(1, 1);
        }

        public async Task<IList<SupplementaryDataLooseModel>> GetESFRecordsFromFile(SourceFileModel sourceFile, CancellationToken cancellationToken)
        {
            List<SupplementaryDataLooseModel> model = null;

            await _getESFLock.WaitAsync(cancellationToken);

            _logger.LogInfo("Try and get csv from Azure blob.");
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    await _storage.GetAsync(sourceFile.FileName, ms, cancellationToken);
                    ms.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(ms))
                    {
                        using (var csvReader = new CsvReader(reader))
                        {
                            csvReader.Configuration.RegisterClassMap(new ESFMapper());
                            csvReader.Configuration.TrimOptions = TrimOptions.Trim;
                            model = csvReader.GetRecords<SupplementaryDataLooseModel>().ToList();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get and deserialise ESF from storage, key: {sourceFile.FileName}", ex);
                if (ex is ValidationException)
                {
                    throw;
                }
            }
            finally
            {
                _getESFLock.Release();
            }

            return model;
        }
    }
}
