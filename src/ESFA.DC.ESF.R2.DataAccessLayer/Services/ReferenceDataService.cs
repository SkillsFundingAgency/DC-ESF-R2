using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using Microsoft.EntityFrameworkCore.Internal;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IReferenceDataRepository _referenceDataRepository;
        private readonly IReferenceDataCache _referenceDataCache;
        private readonly IFCSRepository _fcsRepository;

        public ReferenceDataService(
            IReferenceDataRepository referenceDataRepository,
            IReferenceDataCache referenceDataCache,
            IFCSRepository fcsRepository)
        {
            _referenceDataRepository = referenceDataRepository;
            _referenceDataCache = referenceDataCache;
            _fcsRepository = fcsRepository;
        }

        public int CurrentPeriod { get; set; }

        public string GetPostcodeVersion(CancellationToken cancellationToken)
        {
            return _referenceDataRepository.GetPostcodeVersion(cancellationToken);
        }

        public string GetLarsVersion(CancellationToken cancellationToken)
        {
            return _referenceDataRepository.GetLarsVersion(cancellationToken);
        }

        public string GetOrganisationVersion(CancellationToken cancellationToken)
        {
            return _referenceDataRepository.GetOrganisationVersion(cancellationToken);
        }

        public string GetProviderName(int ukPrn, CancellationToken cancellationToken)
        {
            return _referenceDataCache.GetProviderName(ukPrn, cancellationToken);
        }

        public IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken)
        {
            return _referenceDataCache.GetUlnLookup(searchUlns, cancellationToken);
        }

        public IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            string conRefNum,
            IList<string> deliverableCodes)
        {
            return _referenceDataCache.GetDeliverableUnitCosts(conRefNum, deliverableCodes);
        }

        public LarsLearningDeliveryModel GetLarsLearningDelivery(string learnAimRef)
        {
            return _referenceDataCache.GetLarsLearningDelivery(learnAimRef);
        }

        public async Task<IEnumerable<LarsLearningDeliveryModel>> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken)
        {
            var larsLearningDeliveries = _referenceDataCache.GetLarsLearningDelivery(learnAimRefs);

            if (!larsLearningDeliveries.Any())
            {
                await _referenceDataCache.PopulateLarsLearningDeliveries(learnAimRefs, cancellationToken);
            }

            return _referenceDataCache.GetLarsLearningDelivery(learnAimRefs);
        }

        public ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null)
        {
            return _referenceDataCache.GetContractAllocation(conRefNum, deliverableCode, cancellationToken);
        }

        public IEnumerable<FcsDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken)
        {
            var deliverableCodeMappings = _referenceDataCache.GetContractDeliverableCodeMapping(deliverableCodes, cancellationToken);

            if (!deliverableCodeMappings.Any())
            {
                deliverableCodeMappings = _fcsRepository.GetContractDeliverableCodeMapping(deliverableCodes, cancellationToken);
                _referenceDataCache.PopulateContractDeliverableCodeMappings(deliverableCodeMappings);
            }

            return deliverableCodeMappings;
        }

        public async Task<IEnumerable<string>> GetContractAllocationsForUkprn(int ukprn, CancellationToken cancellationToken)
        {
            return await _fcsRepository.GetContractAllocationsForUkprn(ukprn, cancellationToken);
        }
    }
}