using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.FileService.Interface;
using ESFA.DC.ILR.ReferenceDataService.Model;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ESF.ILR1920.ReferenceData
{
    public class Ilr1920ReferenceDataCacheService : IIlrReferenceDataCacheService
    {
        private readonly IFileService _fileService;
        private readonly IJsonSerializationService _jsonSerializationService;
        private readonly IReferenceDataCache _referenceDataCache;

        public Ilr1920ReferenceDataCacheService(
            IJsonSerializationService jsonSerializationService,
            IFileService fileService,
            IReferenceDataCache referenceDataCache)
        {
            _fileService = fileService;
            _jsonSerializationService = jsonSerializationService;
            _referenceDataCache = referenceDataCache;
        }

        public async Task PopulateCacheFromJson(JobContextModel jobContextMessage, CancellationToken cancellationToken)
        {
            ReferenceDataRoot referenceDataRoot;

            using (var stream = await _fileService.OpenReadStreamAsync(jobContextMessage.IlrReferenceDataKey, jobContextMessage.BlobContainerName, cancellationToken))
            {
                stream.Position = 0;

                referenceDataRoot = _jsonSerializationService.Deserialize<ReferenceDataRoot>(stream);
            }

            var mappings = new List<FcsDeliverableCodeMapping>();
            foreach (var contractAllocation in referenceDataRoot.FCSContractAllocations)
            {
                foreach (var contractDeliverable in contractAllocation.FCSContractDeliverables
                    .Where(cd => cd.ExternalDeliverableCode != null))
                {
                    mappings.Add(new FcsDeliverableCodeMapping
                    {
                        DeliverableName = contractDeliverable.DeliverableDescription,
                        FcsDeliverableCode = contractDeliverable.DeliverableCode.ToString(),
                        ExternalDeliverableCode = contractDeliverable.ExternalDeliverableCode,
                        FundingStreamPeriodCode = contractAllocation.FundingStreamPeriodCode
                    });
                }
            }

            _referenceDataCache.PopulateContractDeliverableCodeMappings(mappings);

            await _referenceDataCache.PopulateDeliverableUnitCosts(ESFConstants.UnitCostDeliverableCodes, jobContextMessage.UkPrn, cancellationToken);
        }
    }
}