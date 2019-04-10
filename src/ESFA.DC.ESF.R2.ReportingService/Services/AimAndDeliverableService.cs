using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.ReportingService.Comparers;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR.DataService.Interfaces.Services;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class AimAndDeliverableService : IAimAndDeliverableService
    {
        private const string FundingStreamPeriodCode = "ESF1420";

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
            List<AimAndDeliverableModel> reportData = null;

            var providerConRefNumbers = (await _validLearnerDataService.GetLearnerConRefNumbers(ukPrn, cancellationToken)).ToList();

            var conRefNumbers = providerConRefNumbers.Where(IsConRefNumberRound2).ToList();

            var validLearners = (await _validLearnerDataService.GetLearnerDetails(ukPrn, conRefNumbers, cancellationToken)).ToList();
            var fm70LearningDeliveries = (await _fm70DataService.GetLearningDeliveries(ukPrn, cancellationToken)).ToList();

            var fm70Outcomes = (await _fm70DataService.GetOutcomes(ukPrn, cancellationToken)).ToList();
            var outcomes = (await _validLearnerDataService.GetDPOutcomes(ukPrn, cancellationToken)).ToList();

            var learnAimRefs = validLearners.Select(ld => ld.LearnAimRef).ToList();
            var deliverableCodes = fm70LearningDeliveries.SelectMany(d => d.Fm70LearningDeliveryDeliverables).Select(ldd => ldd.DeliverableCode).ToList();

            var fcsCodeMappings =
                _referenceDataService.GetContractDeliverableCodeMapping(deliverableCodes, cancellationToken).ToList();
            var larsDeliveries = _referenceDataService.GetLarsLearningDelivery(learnAimRefs).ToList();

            foreach (var learner in validLearners)
            {
                var fm70Delivery = fm70LearningDeliveries
                    .FirstOrDefault(d => d.LearnRefNumber.CaseInsensitiveEquals(learner.LearnRefNumber) &&
                                d.AimSeqNumber == learner.AimSeqNumber);

                var outcomeType = fm70Delivery?.EligibleProgressionOutcomeType;
                var outcomeCode = fm70Delivery?.EligibleProgressionOutcomeCode;
                var outcomeStartDate = fm70Delivery?.EligibleProgressionOutomeStartDate;
                var outcome = outcomes?.SingleOrDefault(o => o.OutType.CaseInsensitiveEquals(outcomeType)
                                                             && o.OutCode == outcomeCode
                                                             && o.OutStartDate == outcomeStartDate);

                var fm70Outcome = fm70Outcomes?.SingleOrDefault(o => o.OutType.CaseInsensitiveEquals(outcomeType)
                                                                     && o.OutCode == outcomeCode
                                                                     && o.OutStartDate == outcomeStartDate);

                var larsDelivery = larsDeliveries?.SingleOrDefault(l => l.LearnAimRef == learner.LearnAimRef);

                if (fm70Delivery?.Fm70LearningDeliveryDeliverables == null ||
                    !fm70Delivery.Fm70LearningDeliveryDeliverables.Any())
                {
                    var model = new AimAndDeliverableModel
                    {
                        ApplicWeightFundRate = fm70Delivery?.ApplicWeightFundRate,
                        AimValue = fm70Delivery?.AimValue,
                        LearnAimRefTitle = larsDelivery?.LearnAimRefTitle,
                        NotionalNVQLevelv2 = larsDelivery?.NotionalNVQLevelv2,
                        SectorSubjectAreaTier2 = larsDelivery?.SectorSubjectAreaTier2,
                        AdjustedAreaCostFactor = fm70Delivery?.AdjustedAreaCostFactor,
                        AdjustedPremiumFactor = fm70Delivery?.AdjustedPremiumFactor,
                        LDESFEngagementStartDate = fm70Delivery?.LdEsfEngagementStartDate,
                        LatestPossibleStartDate = fm70Delivery?.LatestPossibleStartDate,
                        EligibleProgressionOutomeStartDate = fm70Delivery?.EligibleProgressionOutomeStartDate,
                        EligibleOutcomeEndDate = outcome?.OutEndDate,
                        EligibleOutcomeCollectionDate = outcome?.OutCollDate,
                        EligibleOutcomeDateProgressionLength = fm70Outcome?.OutcomeDateForProgression,
                        EligibleProgressionOutcomeType = fm70Delivery?.EligibleProgressionOutcomeType,
                        EligibleProgressionOutcomeCode = fm70Delivery?.EligibleProgressionOutcomeCode
                    };

                    reportData.Add(model);
                    continue;
                }

                foreach (var deliverable in fm70Delivery.Fm70LearningDeliveryDeliverables)
                {
                    var deliverableCode = deliverable?.DeliverableCode;
                    var fcsMapping = fcsCodeMappings?.SingleOrDefault(f =>
                        f.ExternalDeliverableCode == deliverableCode
                        && f.FundingStreamPeriodCode == FundingStreamPeriodCode);

                    var fm70Periods = fm70LearningDeliveries
                        .SelectMany(ld => ld.Fm70LearningDeliveryDeliverablePeriods)
                        ?.Where(p =>
                        p.LearnRefNumber == learner.LearnRefNumber
                        && p.AimSeqNumber == learner.AimSeqNumber
                        && p.DeliverableCode == deliverableCode);

                    foreach (var period in fm70Periods)
                    {
                        var total = (period?.StartEarnings ?? 0) + (period?.AchievementEarnings ?? 0)
                                + (period?.AdditionalProgCostEarnings ?? 0) + (period?.ProgressionEarnings ?? 0);
                        if (period.ReportingVolume == 0 && period.DeliverableVolume == 0 && total == 0)
                        {
                            continue;
                        }

                        var reportModel = new AimAndDeliverableModel
                        {
                            DeliverableCode = deliverableCode,
                            DeliverableName = fcsMapping?.DeliverableName,
                            DeliverableUnitCost = deliverable?.DeliverableUnitCost,
                            ApplicWeightFundRate = fm70Delivery?.ApplicWeightFundRate,
                            AimValue = fm70Delivery?.AimValue,
                            LearnAimRefTitle = larsDelivery?.LearnAimRefTitle,
                            NotionalNVQLevelv2 = larsDelivery?.NotionalNVQLevelv2,
                            SectorSubjectAreaTier2 = larsDelivery?.SectorSubjectAreaTier2,
                            AdjustedAreaCostFactor = fm70Delivery?.AdjustedAreaCostFactor,
                            AdjustedPremiumFactor = fm70Delivery?.AdjustedPremiumFactor,
                            LDESFEngagementStartDate = fm70Delivery?.LdEsfEngagementStartDate,
                            Outcome = learner.Outcome,
                            AddHours = learner.AddHours,
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

                        reportData.Add(reportModel);
                    }
                }
            }

            reportData.Sort(_comparer);

            return reportData;
        }

        public bool IsConRefNumberRound2(string conRefNumber)
        {
            var numericString = conRefNumber.Replace(ESFConstants.ConRefNumberPrefix, string.Empty);

            if (!int.TryParse(numericString, out var contractNumber))
            {
                return false;
            }

            return contractNumber >= ESFConstants.ESFRound2StartConRefNumber;
        }
    }
}