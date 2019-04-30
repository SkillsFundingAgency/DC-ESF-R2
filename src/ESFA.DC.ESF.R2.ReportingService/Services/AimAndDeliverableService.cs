using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Comparers;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class AimAndDeliverableService : IAimAndDeliverableService
    {
        private const string FundingStreamPeriodCode = "ESF1420";

        private const int FundingModel = 70;

        private readonly string[] _reportMonths =
        {
            "Aug-18",
            "Sep-18",
            "Oct-18",
            "Nov-18",
            "Dec-18",
            "Jan-19",
            "Feb-19",
            "Mar-19",
            "Apr-19",
            "May-19",
            "Jun-19",
            "Jul-19",
        };

        private readonly IValidLearnerDataService _validLearnerDataService;
        private readonly IReferenceDataService _referenceDataService;
        private readonly IFm70DataService _fm70DataService;

        private readonly AimAndDeliverableComparer _comparer;

        public AimAndDeliverableService(
            IFm70DataService fm70DataService,
            IValidLearnerDataService validLearnerDataService,
            IReferenceDataService referenceDataService,
            IAimAndDeliverableComparer comparer)
        {
            _fm70DataService = fm70DataService;
            _validLearnerDataService = validLearnerDataService;
            _referenceDataService = referenceDataService;
            _comparer = comparer as AimAndDeliverableComparer;
        }

        public async Task<IEnumerable<AimAndDeliverableModel>> GetAimAndDeliverableModel(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var reportData = new List<AimAndDeliverableModel>();

            var providerConRefNumbers = (await _validLearnerDataService.GetLearnerConRefNumbers(ukPrn, cancellationToken)).ToList();

            var conRefNumbers = providerConRefNumbers.Where(IsConRefNumberRound2).ToList();

            var validLearners = (await _validLearnerDataService.GetLearnerDetails(ukPrn, FundingModel, conRefNumbers, cancellationToken))
                .ToDictionary(vl => new ValidLearnerKey(vl.LearnRefNumber, vl.AimSeqNumber ?? 0), vl => vl);

            var fm70LearningDeliveries =
                (await _fm70DataService.GetLearningDeliveries(ukPrn, cancellationToken)).ToList();

            var learningDeliveryFams =
                (await _validLearnerDataService.GetLearningDeliveryFams(ukPrn, Constants.LearningDeliveryFamTypeRES, cancellationToken)).ToList();

            var learnerMonitorings = (await _validLearnerDataService.GetLearnerMonitorings(ukPrn, cancellationToken))
                .ToDictionary(
                    lm => new LearnerMonitoringKey(lm.LearnRefNumber, lm.ProvSpecLearnMonOccur),
                    lm => lm);

            var deliveryMonitorings = (await _validLearnerDataService.GetDeliveryMonitorings(ukPrn, cancellationToken))
                .ToDictionary(
                    dm => new DeliveryMonitoringKey(dm.LearnRefNumber, dm.ProvSpecDelMonOccur, dm.AimSeqNumber),
                    dm => dm);

            var fm70Outcomes = (await _fm70DataService.GetOutcomes(ukPrn, cancellationToken))
                .ToDictionary(
                    oc => new OutcomeKey(
                        oc.LearnRefNumber,
                        oc.OutType,
                        oc.OutCode,
                        oc.OutStartDate),
                    oc => oc);

            var outcomes = (await _validLearnerDataService.GetDPOutcomes(ukPrn, cancellationToken))
                .ToDictionary(
                    oc => new OutcomeKey(
                        oc.LearnRefNumber,
                        oc.OutType,
                        oc.OutCode,
                        oc.OutStartDate),
                    oc => oc);

            var deliverableCodes = fm70LearningDeliveries.Select(ld => ld.DeliverableCode).ToList();

            var fcsCodeMappings =
                _referenceDataService.GetContractDeliverableCodeMapping(deliverableCodes, cancellationToken).ToList();

            var learnAimRefs = validLearners.Select(ld => ld.Value.LearnAimRef).ToList();

            var larsDeliveries = _referenceDataService.GetLarsLearningDelivery(learnAimRefs)
                .ToDictionary(t => t.LearnAimRef, t => t);

            foreach (var fm70Delivery in fm70LearningDeliveries)
            {
                var key = new ValidLearnerKey(fm70Delivery.LearnRefNumber, fm70Delivery.AimSeqNumber);
                validLearners.TryGetValue(key, out var learningDelivery);

                if (learningDelivery == null)
                {
                    continue;
                }

                DpOutcome outcome = null;
                Fm70DpOutcome fm70Outcome = null;
                if (!string.IsNullOrWhiteSpace(fm70Delivery.EligibleProgressionOutcomeType)
                   && fm70Delivery.EligibleProgressionOutcomeCode != null
                   && fm70Delivery.EligibleProgressionOutomeStartDate != null)
                {
                    var outcomeKey = new OutcomeKey(
                        fm70Delivery.LearnRefNumber,
                        fm70Delivery.EligibleProgressionOutcomeType,
                        fm70Delivery.EligibleProgressionOutcomeCode ?? 0,
                        fm70Delivery.EligibleProgressionOutomeStartDate ?? DateTime.MinValue);

                    outcomes.TryGetValue(outcomeKey, out outcome);

                    fm70Outcomes.TryGetValue(outcomeKey, out fm70Outcome);
                }

                larsDeliveries.TryGetValue(learningDelivery.LearnRefNumber, out var larsDelivery);

                var fam = learningDeliveryFams.FirstOrDefault(ldf =>
                    ldf.LearnRefNumber.CaseInsensitiveEquals(learningDelivery.LearnRefNumber)
                    && ldf.AimSeqNumber == learningDelivery.AimSeqNumber);

                if (!fm70Delivery.Fm70LearningDeliveryDeliverables.Any())
                {
                    var model = GetAimAndDeliverableModel(learningDelivery, fm70Delivery, larsDelivery, outcome, fm70Outcome, fam, learnerMonitorings, deliveryMonitorings);

                    reportData.Add(model);
                    continue;
                }

                foreach (var deliverable in fm70Delivery.Fm70LearningDeliveryDeliverables)
                {
                    var deliverableCode = deliverable.DeliverableCode;

                    var fcsMapping = fcsCodeMappings?.SingleOrDefault(f =>
                        f.ExternalDeliverableCode.CaseInsensitiveEquals(deliverableCode)
                        && f.FundingStreamPeriodCode.CaseInsensitiveEquals(FundingStreamPeriodCode));

                    var fm70Periods = fm70Delivery.Fm70LearningDeliveryDeliverablePeriods.Where(delivery =>
                        delivery.LearnRefNumber.CaseInsensitiveEquals(fm70Delivery.LearnRefNumber)
                        && delivery.AimSeqNumber == fm70Delivery.AimSeqNumber
                        && delivery.DeliverableCode.CaseInsensitiveEquals(deliverableCode));

                    foreach (var period in fm70Periods)
                    {
                        var total = (period.StartEarnings ?? 0) + (period.AchievementEarnings ?? 0)
                                + (period.AdditionalProgCostEarnings ?? 0) + (period.ProgressionEarnings ?? 0);
                        if (period.ReportingVolume == 0 && period.DeliverableVolume == 0 && total == 0)
                        {
                            continue;
                        }

                        var model = GetAimAndDeliverableModel(
                            learningDelivery,
                            fm70Delivery,
                            larsDelivery,
                            outcome,
                            fm70Outcome,
                            fam,
                            learnerMonitorings,
                            deliveryMonitorings,
                            deliverable,
                            period,
                            fcsMapping,
                            total);

                        reportData.Add(model);
                    }
                }
            }

            reportData.Sort(_comparer);

            return reportData;
        }

        private AimAndDeliverableModel GetAimAndDeliverableModel(
            LearnerDetails learnerDetails,
            Fm70LearningDelivery fm70Delivery,
            LarsLearningDeliveryModel larsDelivery,
            DpOutcome outcome,
            Fm70DpOutcome fm70Outcome,
            LearningDeliveryFam fam,
            IDictionary<LearnerMonitoringKey, ProviderSpecLearnerMonitoring> learnerMonitorings,
            IDictionary<DeliveryMonitoringKey, ProviderSpecDeliveryMonitoring> deliveryMonitorings,
            Fm70LearningDeliveryDeliverable deliverable = null,
            Fm70LearningDeliveryDeliverablePeriod period = null,
            FcsDeliverableCodeMapping fcsMapping = null,
            decimal? total = null)
        {
            var model = new AimAndDeliverableModel
            {
                LearnRefNumber = learnerDetails.LearnRefNumber,
                ULN = learnerDetails.Uln,
                AimSeqNumber = learnerDetails.AimSeqNumber,
                ConRefNumber = learnerDetails.ConRefNumber,
                DeliverableCode = deliverable?.DeliverableCode,
                DeliverableName = fcsMapping?.DeliverableName,
                LearnAimRef = learnerDetails.LearnAimRef,
                DeliverableUnitCost = deliverable?.DeliverableUnitCost,
                ApplicWeightFundRate = fm70Delivery?.ApplicWeightFundRate,
                AimValue = fm70Delivery?.AimValue,
                LearnAimRefTitle = larsDelivery?.LearnAimRefTitle,
                PMUKPRN = learnerDetails.Pmukprn,
                CampId = learnerDetails.CampId,
                ProvSpecLearnMonA = GetLearnerMonitoring(learnerMonitorings, learnerDetails.LearnRefNumber, "A"),
                ProvSpecLearnMonB = GetLearnerMonitoring(learnerMonitorings, learnerDetails.LearnRefNumber, "B"),
                SWSupAimId = learnerDetails.SwsupAimId,
                NotionalNVQLevelv2 = larsDelivery?.NotionalNVQLevelv2,
                SectorSubjectAreaTier2 = larsDelivery?.SectorSubjectAreaTier2,
                AdjustedAreaCostFactor = fm70Delivery?.AdjustedAreaCostFactor,
                AdjustedPremiumFactor = fm70Delivery?.AdjustedPremiumFactor,
                LearnStartDate = learnerDetails.LearnStartDate,
                LDESFEngagementStartDate = fm70Delivery?.LdEsfEngagementStartDate,
                LearnPlanEndDate = learnerDetails.LearnPlanEndDate,
                CompStatus = learnerDetails.CompStatus,
                LearnActEndDate = learnerDetails.LearnActEndDate,
                Outcome = learnerDetails.Outcome,
                AddHours = learnerDetails.AddHours,
                LearnDelFAMCode = fam?.LearnDelFamCode,
                ProvSpecDelMonA = GetDeliveryMonitoring(deliveryMonitorings, learnerDetails.LearnRefNumber, learnerDetails.AimSeqNumber, "A"),
                ProvSpecDelMonB = GetDeliveryMonitoring(deliveryMonitorings, learnerDetails.LearnRefNumber, learnerDetails.AimSeqNumber, "B"),
                ProvSpecDelMonC = GetDeliveryMonitoring(deliveryMonitorings, learnerDetails.LearnRefNumber, learnerDetails.AimSeqNumber, "C"),
                ProvSpecDelMonD = GetDeliveryMonitoring(deliveryMonitorings, learnerDetails.LearnRefNumber, learnerDetails.AimSeqNumber, "D"),
                PartnerUKPRN = learnerDetails.PartnerUkprn,
                DelLocPostCode = learnerDetails.DelLocPostCode,
                LatestPossibleStartDate = fm70Delivery?.LatestPossibleStartDate,
                EligibleProgressionOutomeStartDate = fm70Delivery?.EligibleProgressionOutomeStartDate,
                EligibleOutcomeEndDate = outcome?.OutEndDate,
                EligibleOutcomeCollectionDate = outcome?.OutCollDate,
                EligibleOutcomeDateProgressionLength = fm70Outcome?.OutcomeDateForProgression,
                EligibleProgressionOutcomeType = fm70Delivery?.EligibleProgressionOutcomeType,
                EligibleProgressionOutcomeCode = fm70Delivery?.EligibleProgressionOutcomeCode,
                Period = _reportMonths[period?.Period - 1 ?? 0],
                DeliverableVolume = period?.DeliverableVolume,
                StartEarnings = period?.StartEarnings,
                AchievementEarnings = period?.AchievementEarnings,
                AdditionalProgCostEarnings = period?.AdditionalProgCostEarnings,
                ProgressionEarnings = period?.ProgressionEarnings,
                TotalEarnings = total
            };
            return model;
        }

        private string GetLearnerMonitoring(
            IDictionary<LearnerMonitoringKey, ProviderSpecLearnerMonitoring> monitorings,
            string learnRefNumber,
            string provSpecLearnMonOccur)
        {
            monitorings.TryGetValue(new LearnerMonitoringKey(learnRefNumber, provSpecLearnMonOccur), out var monitoring);
            return monitoring?.ProvSpecLearnMon;
        }

        private string GetDeliveryMonitoring(
            IDictionary<DeliveryMonitoringKey, ProviderSpecDeliveryMonitoring> monitorings,
            string learnRefNumber,
            int? aimSeqNumber,
            string provSpecLearnMonOccur)
        {
            monitorings.TryGetValue(new DeliveryMonitoringKey(learnRefNumber, provSpecLearnMonOccur, aimSeqNumber ?? 0), out var monitoring);
            return monitoring?.ProvSpecDelMon;
        }

        private bool IsConRefNumberRound2(string conRefNumber)
        {
            var numericString = conRefNumber.Replace(ESFConstants.ConRefNumberPrefix, string.Empty);

            if (!int.TryParse(numericString, out var contractNumber))
            {
                return false;
            }

            return contractNumber >= ESFConstants.ESFRound2StartConRefNumber;
        }

        private struct ValidLearnerKey
        {
            private readonly string _learnRefNumber;

            private readonly int _aimSeqNumber;

            public ValidLearnerKey(string learnRefNumber, int aimSeqNumber)
            {
                _learnRefNumber = learnRefNumber;
                _aimSeqNumber = aimSeqNumber;
            }

            public override bool Equals(object key)
            {
                var type = (ValidLearnerKey)key;
                return type._learnRefNumber.CaseInsensitiveEquals(_learnRefNumber)
                       && type._aimSeqNumber.Equals(_aimSeqNumber);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hash = 17;
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_learnRefNumber);
                    hash = (hash * 23) + _aimSeqNumber.GetHashCode();
                    return hash;
                }
            }
        }

        private struct OutcomeKey
        {
            private readonly string _outcomeType;

            private readonly string _learnRefNumber;

            private readonly long _outcomeCode;

            private readonly DateTime _outcomeStartDate;

            public OutcomeKey(string learnRefNumber, string outcomeType, long outcomeCode, DateTime outcomeStartDate)
            {
                _outcomeType = outcomeType;
                _learnRefNumber = learnRefNumber;
                _outcomeCode = outcomeCode;
                _outcomeStartDate = outcomeStartDate;
            }

            public override bool Equals(object key)
            {
                var type = (OutcomeKey)key;
                return type._learnRefNumber.CaseInsensitiveEquals(_learnRefNumber)
                       && type._outcomeType.CaseInsensitiveEquals(_outcomeType)
                       && type._outcomeCode.Equals(_outcomeCode)
                       && type._outcomeStartDate.Equals(_outcomeStartDate);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hash = 17;
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_learnRefNumber);
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_outcomeType);
                    hash = (hash * 23) + _outcomeCode.GetHashCode();
                    hash = (hash * 23) + _outcomeStartDate.GetHashCode();
                    return hash;
                }
            }
        }

        private struct LearnerMonitoringKey
        {
            private readonly string _learnRefNumber;

            private readonly string _provSpecLearnMonOccur;

            public LearnerMonitoringKey(string learnRefNumber, string provSpecDelMonOccur)
            {
                _learnRefNumber = learnRefNumber;
                _provSpecLearnMonOccur = provSpecDelMonOccur;
            }

            public override bool Equals(object key)
            {
                var type = (LearnerMonitoringKey)key;
                return type._learnRefNumber.CaseInsensitiveEquals(_learnRefNumber)
                       && type._provSpecLearnMonOccur.CaseInsensitiveEquals(_provSpecLearnMonOccur);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hash = 17;
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_learnRefNumber);
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_provSpecLearnMonOccur);
                    return hash;
                }
            }
        }

        private struct DeliveryMonitoringKey
        {
            private readonly string _learnRefNumber;

            private readonly string _provSpecDelMonOccur;

            private readonly int _aimSeqNumber;

            public DeliveryMonitoringKey(string learnRefNumber, string provSpecDelMonOccur, int aimSeqNumber)
            {
                _learnRefNumber = learnRefNumber;
                _provSpecDelMonOccur = provSpecDelMonOccur;
                _aimSeqNumber = aimSeqNumber;
            }

            public override bool Equals(object key)
            {
                var type = (DeliveryMonitoringKey)key;
                return type._learnRefNumber.CaseInsensitiveEquals(_learnRefNumber)
                       && type._provSpecDelMonOccur.CaseInsensitiveEquals(_provSpecDelMonOccur)
                       && type._aimSeqNumber.Equals(_aimSeqNumber);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hash = 17;
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_learnRefNumber);
                    hash = (hash * 23) + StringComparer.OrdinalIgnoreCase.GetHashCode(_provSpecDelMonOccur);
                    hash = (hash * 23) + _aimSeqNumber.GetHashCode();
                    return hash;
                }
            }
        }
    }
}