using System.Collections.Generic;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Services
{
    public class ReferenceDataService : IReferenceDataService
    {
        private readonly IReferenceDataRepository _referenceDataRepository;
        private readonly IReferenceDataCache _referenceDataCache;

        public ReferenceDataService(
            IReferenceDataRepository referenceDataRepository,
            IReferenceDataCache referenceDataCache)
        {
            _referenceDataRepository = referenceDataRepository;
            _referenceDataCache = referenceDataCache;
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
            IList<string> deliverableCodes,
            int ukPrn,
            CancellationToken cancellationToken)
        {
            return _referenceDataCache.GetDeliverableUnitCosts(deliverableCodes, ukPrn, cancellationToken);
        }

        public LarsLearningDeliveryModel GetLarsLearningDelivery(string learnAimRef)
        {
            return _referenceDataCache.GetLarsLearningDelivery(learnAimRef);
        }

        public ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null)
        {
            return _referenceDataCache.GetContractAllocation(conRefNum, deliverableCode, cancellationToken);
        }
    }
}