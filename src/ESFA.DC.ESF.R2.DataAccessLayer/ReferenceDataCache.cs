using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            LarsLearnAimRefs = new Dictionary<string, LarsLearningDeliveryModel>(StringComparer.OrdinalIgnoreCase);
            ContractAllocations = new List<ContractAllocationCacheModel>();

            _referenceDataRepository = referenceDataRepository;
            _fcsRepository = fcsRepository;
            _esfRepository = esfRepository;
        }

        private HashSet<long> Ulns { get; }

        private List<FcsDeliverableCodeMapping> CodeMappings { get; }

        private List<ContractAllocationCacheModel> ContractAllocations { get; }

        private IDictionary<int, string> ProviderNameByUkprn { get; }

        private IDictionary<string, LarsLearningDeliveryModel> LarsLearnAimRefs { get; }

        private List<DeliverableUnitCost> DeliverableUnitCosts { get; }

        public string GetProviderName(int ukPrn, CancellationToken cancellationToken)
        {
            if (ProviderNameByUkprn.ContainsKey(ukPrn))
            {
                return ProviderNameByUkprn[ukPrn];
            }

            var name = _referenceDataRepository.GetProviderName(ukPrn, cancellationToken);
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }

            ProviderNameByUkprn[ukPrn] = name;

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

            if (!uncached.Any())
            {
                return CodeMappings;
            }

            PopulateContractDeliverableCodeMappings(uncached, cancellationToken);

            return CodeMappings;
        }

        public void PopulateContractDeliverableCodeMappings(IEnumerable<string> uncached, CancellationToken cancellationToken)
        {
            var mappings = _fcsRepository.GetContractDeliverableCodeMapping(uncached, cancellationToken);
            PopulateContractDeliverableCodeMappings(mappings);
        }

        public void PopulateContractDeliverableCodeMappings(IEnumerable<FcsDeliverableCodeMapping> mappings)
        {
            foreach (var mapping in mappings)
            {
                CodeMappings.Add(mapping);
            }
        }

        public IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken)
        {
            var uniqueUlns = searchUlns.Distinct().ToList();
            var unknownUlns = uniqueUlns.Where(uln => Ulns.All(u => u != uln)).ToList();

            if (!unknownUlns.Any())
            {
                return Ulns.Where(u => uniqueUlns.Contains(u));
            }

            PopulateUlnLookup(unknownUlns, cancellationToken);

           return Ulns.Where(u => unknownUlns.Contains(u));
        }

        public void PopulateUlnLookup(IEnumerable<long?> unknownUlns, CancellationToken cancellationToken)
        {
            var result = _referenceDataRepository.GetUlnLookup(unknownUlns, cancellationToken);
            if (result != null)
            {
                Ulns.UnionWith(result);
            }
        }

        public ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null)
        {
            if (ukPrn == null ||
                ContractAllocations.Any(ca => ca.DeliverableCode == deliverableCode &&
                               ca.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum)))
            {
                return ContractAllocations?.FirstOrDefault(ca => ca.DeliverableCode == deliverableCode &&
                                                                 ca.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum));
            }

            PopulateContractAllocations(conRefNum, deliverableCode, cancellationToken, ukPrn);

            return ContractAllocations?.FirstOrDefault(ca => ca.DeliverableCode == deliverableCode &&
                                                            ca.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum));
        }

        public void PopulateContractAllocations(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn)
        {
            var contractAllocation =
                _fcsRepository.GetContractAllocation(conRefNum, deliverableCode, cancellationToken, ukPrn);

            if (contractAllocation != null)
            {
                ContractAllocations.Add(contractAllocation);
            }
        }

        public IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            string conRefNum,
            IList<string> deliverableCodes)
        {
            return DeliverableUnitCosts.Where(duc => duc.ConRefNum.CaseInsensitiveEquals(conRefNum)
                                              && deliverableCodes.Any(dc =>
                                                  dc.CaseInsensitiveEquals(duc.DeliverableCode))).ToList();
        }

        public async Task PopulateDeliverableUnitCosts(
            List<string> newItemsNotInCache,
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var contractRefNumsForProvider = await _esfRepository.GetContractsForProvider(ukPrn.ToString(), cancellationToken);

            foreach (var conRefNum in contractRefNumsForProvider)
            {
                var deliverableUnitCosts =
                    _fcsRepository.GetDeliverableUnitCosts(newItemsNotInCache, ukPrn, conRefNum, cancellationToken);

                if (deliverableUnitCosts != null)
                {
                    DeliverableUnitCosts.AddRange(deliverableUnitCosts);
                }
            }
        }

        public LarsLearningDeliveryModel GetLarsLearningDelivery(string learnAimRef)
        {
            LarsLearnAimRefs.TryGetValue(learnAimRef, out var learningDeliveryModel);

            return learningDeliveryModel;
        }

        public IEnumerable<LarsLearningDeliveryModel> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs)
        {
            var larsLearningDeliveries = new List<LarsLearningDeliveryModel>();

            foreach (var learnAimRef in learnAimRefs)
            {
                LarsLearnAimRefs.TryGetValue(learnAimRef, out var learningDeliveryModel);
                if (learningDeliveryModel != null)
                {
                    larsLearningDeliveries.Add(learningDeliveryModel);
                }
            }

            return larsLearningDeliveries;
        }

        public void PopulateLarsLearningDeliveries(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken)
        {
            var larsLearningDeliveries =
                _referenceDataRepository.GetLarsLearningDelivery(learnAimRefs, cancellationToken);

            if (larsLearningDeliveries == null)
            {
                return;
            }

            foreach (var larsLearningDelivery in larsLearningDeliveries)
            {
                LarsLearnAimRefs.Add(larsLearningDelivery.Key, larsLearningDelivery.Value);
            }
        }
    }
}