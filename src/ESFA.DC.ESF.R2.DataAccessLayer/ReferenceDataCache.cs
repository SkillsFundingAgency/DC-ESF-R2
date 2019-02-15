﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.ULN.Model;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ReferenceData.FCS.Model;

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
            Ulns = new List<UniqueLearnerNumber>();
            CodeMappings = new List<ContractDeliverableCodeMapping>();
            DeliverableUnitCosts = new List<DeliverableUnitCost>();
            ProviderNameByUkprn = new Dictionary<int, string>();
            LarsLearningDeliveries = new List<LARS_LearningDelivery>();
            ContractAllocations = new List<ContractAllocationCacheModel>();

            _referenceDataRepository = referenceDataRepository;
            _fcsRepository = fcsRepository;
            _esfRepository = esfRepository;
        }

        public List<UniqueLearnerNumber> Ulns { get; }

        public List<ContractDeliverableCodeMapping> CodeMappings { get; }

        public List<ContractAllocationCacheModel> ContractAllocations { get; }

        public IDictionary<int, string> ProviderNameByUkprn { get; }

        public List<LARS_LearningDelivery> LarsLearningDeliveries { get; }

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
            var uncached = deliverableCodes.Where(deliverableCode => CodeMappings.All(x => x.ExternalDeliverableCode != deliverableCode)).ToList();

            if (uncached.Any())
            {
                CodeMappings.AddRange(_fcsRepository.GetContractDeliverableCodeMapping(uncached, cancellationToken));
            }

            return CodeMappings;
        }

        public IList<UniqueLearnerNumber> GetUlnLookup(IList<long?> searchUlns, CancellationToken cancellationToken)
        {
            var uniqueUlns = searchUlns.Distinct();
            var unknownUlns = uniqueUlns.Where(uln => Ulns.All(u => u.ULN != uln)).ToList();

            if (unknownUlns.Any())
            {
                Ulns.AddRange(_referenceDataRepository.GetUlnLookup(unknownUlns, cancellationToken));
            }

            return Ulns.Where(x => searchUlns.Contains(x.ULN)).ToList();
        }

        public ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null)
        {
            if (ukPrn != null && !ContractAllocations
                    .Any(ca => ca.DeliverableCode == deliverableCode &&
                               ca.ContractAllocationNumber == conRefNum))
            {
                var contractAllocation =
                    _fcsRepository.GetContractAllocation(conRefNum, deliverableCode, cancellationToken, ukPrn);

                if (contractAllocation != null)
                {
                    ContractAllocations.Add(contractAllocation);
                }
            }

            return ContractAllocations?.FirstOrDefault(ca => ca.DeliverableCode == deliverableCode &&
                                                            ca.ContractAllocationNumber == conRefNum);
        }

        public IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var contractRefNumsForProvider = _esfRepository.GetContractsForProvider(ukPrn.ToString(), cancellationToken).Result;

            var contractDeliverableCodeUnitCosts = DeliverableUnitCosts.Where(uc => contractRefNumsForProvider.Contains(uc.ConRefNum));

            var newItemsNotInCache = deliverableCodes.Where(dc => !contractDeliverableCodeUnitCosts.Any(cache => dc.Contains(cache.DeliverableCode)));
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

        public IList<LARS_LearningDelivery> GetLarsLearningDelivery(
            IList<string> learnAimRefs,
            CancellationToken cancellationToken)
        {
            var uncached = learnAimRefs.Where(learnAimRef => LarsLearningDeliveries.All(x => x.LearnAimRef != learnAimRef)).ToList();

            if (uncached.Any())
            {
                LarsLearningDeliveries.AddRange(_referenceDataRepository.GetLarsLearningDelivery(uncached, cancellationToken));
            }

            return LarsLearningDeliveries.Where(l => learnAimRefs.Contains(l.LearnAimRef)).ToList();
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