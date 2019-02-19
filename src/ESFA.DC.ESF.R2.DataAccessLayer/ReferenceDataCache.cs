using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ESF.R2.Utils;

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
            CodeMappings = new List<FcsDeliverableCodeMapping>();
            DeliverableUnitCosts = new List<DeliverableUnitCost>();
            ProviderNameByUkprn = new Dictionary<int, string>();
            LarsLearnAimRefs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            ContractAllocations = new List<ContractAllocationCacheModel>();

            _referenceDataRepository = referenceDataRepository;
            _fcsRepository = fcsRepository;
            _esfRepository = esfRepository;
        }

        public HashSet<long> Ulns { get; }

        public List<FcsDeliverableCodeMapping> CodeMappings { get; }

        public List<ContractAllocationCacheModel> ContractAllocations { get; }

        public IDictionary<int, string> ProviderNameByUkprn { get; }

        public HashSet<string> LarsLearnAimRefs { get; }

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

        public IEnumerable<FcsDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken)
        {
            var uncached = deliverableCodes
                .Where(deliverableCode => CodeMappings
                    .All(x => !x.ExternalDeliverableCode.CaseInsensitiveEquals(deliverableCode)))
                .ToList();

            if (uncached.Any())
            {
                var mappings = _fcsRepository.GetContractDeliverableCodeMapping(uncached, cancellationToken);
                foreach (var mapping in mappings)
                {
                    CodeMappings.Add(mapping);
                }
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

        public IEnumerable<string> GetLarsLearningDelivery(
            IEnumerable<string> learnAimRefs,
            CancellationToken cancellationToken)
        {
            var uncached = learnAimRefs.Where(learnAimRef => LarsLearnAimRefs.All(x => x != learnAimRef)).ToList();

            if (uncached.Any())
            {
                LarsLearnAimRefs.UnionWith(_referenceDataRepository.GetLarsLearningDelivery(uncached, cancellationToken));
            }

            return LarsLearnAimRefs.Where(l => learnAimRefs.Any(la => la.CaseInsensitiveEquals(l))).ToList();
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