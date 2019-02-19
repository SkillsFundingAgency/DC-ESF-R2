using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ReferenceData.FCS.Model;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class ReferenceDataCache : IReferenceDataCache
    {
        private readonly IReferenceDataRepository _referenceDataRepository;
        private readonly IFCSRepository _fcsRepository;
        private readonly IEsfRepository _esfRepository;

        public ReferenceDataCache(
            IEsfRepository esfRepository,
            IFCSRepository fcsRepository,
            IReferenceDataRepository referenceDataRepository)
        {
            Ulns = new HashSet<long>();
            CodeMappings = new List<ContractDeliverableCodeMapping>();
            DeliverableUnitCosts = new List<DeliverableUnitCost>();
            ProviderNameByUkprn = new Dictionary<int, string>();
            LarsLearningDeliveries = new List<LarsLearningDelivery>();
            ContractAllocations = new List<ContractAllocationCacheModel>();

            _referenceDataRepository = referenceDataRepository;
            _fcsRepository = fcsRepository;
            _esfRepository = esfRepository;
        }

        public HashSet<long> Ulns { get; }

        public List<ContractDeliverableCodeMapping> CodeMappings { get; }

        public List<ContractAllocationCacheModel> ContractAllocations { get; }

        public IDictionary<int, string> ProviderNameByUkprn { get; }

        public List<LarsLearningDelivery> LarsLearningDeliveries { get; }

        public List<DeliverableUnitCost> DeliverableUnitCosts { get; }

        public int CurrentPeriod { get; set; }

        public string GetProviderName(int ukPrn, CancellationToken cancellationToken)
        {
            if (!ProviderNameByUkprn.ContainsKey(ukPrn))
            {
                ProviderNameByUkprn[ukPrn] = _referenceDataRepository.GetProviderName(ukPrn, cancellationToken);
            }

            return ProviderNameByUkprn[ukPrn];
        }

        public IList<ContractDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IList<string> deliverableCodes,
            CancellationToken cancellationToken)
        {
            var uncached = deliverableCodes
                .Where(deliverableCode => CodeMappings
                    .All(x => !x.ExternalDeliverableCode.CaseInsensitiveEquals(deliverableCode)))
                .ToList();

            if (uncached.Any())
            {
                CodeMappings.AddRange(_fcsRepository.GetContractDeliverableCodeMapping(uncached, cancellationToken));
            }

            return CodeMappings;
        }

        public IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken)
        {
            var uniqueUlns = searchUlns.Distinct();
            var unknownUlns = uniqueUlns.Where(uln => Ulns.All(u => u != uln)).ToList();

            if (unknownUlns.Any())
            {
                Ulns.UnionWith(_referenceDataRepository.GetUlnLookup(unknownUlns, cancellationToken));
            }

            return Ulns.Where(u => searchUlns.Contains(u));
        }

        public ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null)
        {
            if (ukPrn != null && !ContractAllocations
                    .Any(ca => ca.DeliverableCode == deliverableCode &&
                               ca.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum)))
            {
                var contractAllocation =
                    _fcsRepository.GetContractAllocation(conRefNum, deliverableCode, cancellationToken, ukPrn);

                if (contractAllocation != null)
                {
                    ContractAllocations.Add(contractAllocation);
                }
            }

            return ContractAllocations?.FirstOrDefault(ca => ca.DeliverableCode == deliverableCode &&
                                                            ca.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum));
        }

        public IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var contractRefNumsForProvider = _esfRepository.GetContractsForProvider(ukPrn.ToString(), cancellationToken).Result;

            var contractDeliverableCodeUnitCosts = DeliverableUnitCosts
                .Where(uc => contractRefNumsForProvider
                    .Any(cp => cp.CaseInsensitiveEquals(uc.ConRefNum)));

            var newItemsNotInCache = deliverableCodes
                .Where(dc => !contractDeliverableCodeUnitCosts
                .Any(cache => dc.CaseInsensitiveEquals(cache.DeliverableCode)));
            if (!newItemsNotInCache.Any())
            {
                return DeliverableUnitCosts;
            }

            foreach (var conRefNum in contractRefNumsForProvider)
            {
                var deliverableUnitCosts =
                    _fcsRepository.GetDeliverableUnitCosts(deliverableCodes, ukPrn, conRefNum, cancellationToken);

                if (deliverableUnitCosts != null)
                {
                    DeliverableUnitCosts.AddRange(deliverableUnitCosts);
                }
            }

            return DeliverableUnitCosts;
        }

        public IList<LarsLearningDelivery> GetLarsLearningDelivery(
            IList<string> learnAimRefs,
            CancellationToken cancellationToken)
        {
            var uncached = learnAimRefs.Where(learnAimRef => LarsLearningDeliveries.All(x => !x.LearnAimRef.CaseInsensitiveEquals(learnAimRef))).ToList();

            if (uncached.Any())
            {
                LarsLearningDeliveries.AddRange(_referenceDataRepository.GetLarsLearningDelivery(uncached, cancellationToken));
            }

            return LarsLearningDeliveries.Where(l => learnAimRefs.Any(la => la.CaseInsensitiveEquals(l.LearnAimRef))).ToList();
        }

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
    }
}