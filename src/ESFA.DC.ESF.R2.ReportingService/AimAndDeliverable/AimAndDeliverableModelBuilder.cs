using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable.Keys;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Interface;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable
{
    public class AimAndDeliverableModelBuilder : IAimAndDeliverableModelBuilder
    {
        private const int RoundTwoContractStartId = 5000;
        private const string EsfContractPrefix = "ESF-";

        public IEnumerable<AimAndDeliverableReportRow> Build(
            IEsfJobContext esfJobContext,
            ICollection<LearningDelivery> learningDeliveries,
            ICollection<DPOutcome> dpOutcomes,
            ICollection<ESFLearningDeliveryDeliverablePeriod> deliverablePeriods,
            ICollection<ESFDPOutcome> esfDpOutcomes,
            ICollection<LARSLearningDelivery> larsLearningDeliveries,
            ICollection<FCSDeliverableCodeMapping> fcsDeliverableCodeMappings)
        {
            return BuildReportRows(
                esfJobContext.StartCollectionYearAbbreviation,
                esfJobContext.EndCollectionYearAbbreviation,
                learningDeliveries,
                dpOutcomes,
                deliverablePeriods,
                esfDpOutcomes,
                larsLearningDeliveries,
                fcsDeliverableCodeMappings)
                .OrderBy(x => x.LearningDelivery?.LearnRefNumber)
                .ThenBy(x => x.LearningDelivery?.ConRefNumber)
                .ThenBy(x => x.LearningDelivery?.LearnStartDate)
                .ThenBy(x => x.LearningDelivery?.AimSeqNumber)
                .ThenBy(x => x.DeliverablePeriod?.Period)
                .ThenBy(x => x.DeliverablePeriod?.DeliverableCode);
        }

        public IEnumerable<AimAndDeliverableReportRow> BuildReportRows(
            string startYear,
            string endYear,
            ICollection<LearningDelivery> learningDeliveries,
            ICollection<DPOutcome> dpOutcomes,
            ICollection<ESFLearningDeliveryDeliverablePeriod> deliverablePeriods,
            ICollection<ESFDPOutcome> esfDpOutcomes,
            ICollection<LARSLearningDelivery> larsLearningDeliveries,
            ICollection<FCSDeliverableCodeMapping> fcsDeliverableCodeMappings)
        {
            var periodMonthLookup = BuildPeriodReportMonthLookup(startYear, endYear);
            var fcsDeliverableCodeNameLookup = BuildFcsDeliverableCodeNameLookup(fcsDeliverableCodeMappings);
            var larsLearningDeliveryLookup = BuildLarsLearningDeliveryLookup(larsLearningDeliveries);
            var dpOutcomeLookup = BuildDpOutcomeLookup(dpOutcomes);
            var esfDpOutcomeLookup = BuildESFDpOutcomeLookup(esfDpOutcomes);
            var learningDeliverablePeriodLookup = BuildEsfLearningDeliveryDeliverableLookup(deliverablePeriods);

            var roundTwoLearningDeliveries = learningDeliveries.Where(ld => ld != null && IsRoundTwoContract(ld.ConRefNumber));

            foreach (var learningDelivery in roundTwoLearningDeliveries)
            {
                DPOutcome dpOutcome = null;
                ESFDPOutcome esfDpOutcome = null;

                if (IsDpOutcomeKey(learningDelivery))
                {
                    var dpOutcomeKey = BuildDpOutcomeKeyForLearningDelivery(learningDelivery);

                    dpOutcome = dpOutcomeLookup.GetValueOrDefault(dpOutcomeKey);
                    esfDpOutcome = esfDpOutcomeLookup.GetValueOrDefault(dpOutcomeKey);
                }

                var larsLearningDelivery = larsLearningDeliveryLookup.GetValueOrDefault(learningDelivery.LearnAimRef);

                var learningDeliverableKey = new ESFLearningDeliveryDeliverableKey(learningDelivery.LearnRefNumber, learningDelivery.AimSeqNumber);

                var deliverablePeriodsForLearningDelivery = learningDeliverablePeriodLookup.GetValueOrDefault(learningDeliverableKey);

                if (deliverablePeriodsForLearningDelivery != null && deliverablePeriodsForLearningDelivery.Any())
                {
                    foreach (var deliverablePeriod in deliverablePeriods)
                    {
                        var deliverableName = fcsDeliverableCodeNameLookup.GetValueOrDefault(deliverablePeriod.DeliverableCode);
                        var reportMonth = periodMonthLookup.GetValueOrDefault(deliverablePeriod.Period);

                        if (DisplayOnReport(deliverablePeriod))
                        {
                            yield return BuildRow(learningDelivery, dpOutcome, esfDpOutcome, larsLearningDelivery, deliverablePeriod, deliverableName, reportMonth);
                        }
                    }
                }
                else
                {
                    yield return BuildRow(learningDelivery, dpOutcome, esfDpOutcome, larsLearningDelivery);
                }
            }
        }

        public bool DisplayOnReport(ESFLearningDeliveryDeliverablePeriod period) =>
            (period.DeliverableVolume == 1 || period.TotalEarnings != 0)
        || (period.DeliverableVolume == 0 && period.TotalEarnings == 0 && period.ReportingVolume == 1);

        // BR3
        public IDictionary<int, string> BuildPeriodReportMonthLookup(string start, string end)
            => new Dictionary<int, string>()
            {
                [1] = $"Aug-{start}",
                [2] = $"Sep-{start}",
                [3] = $"Oct-{start}",
                [4] = $"Nov-{start}",
                [5] = $"Dec-{start}",
                [6] = $"Jan-{end}",
                [7] = $"Feb-{end}",
                [8] = $"Mar-{end}",
                [9] = $"Apr-{end}",
                [10] = $"May-{end}",
                [11] = $"Jun-{end}",
                [12] = $"Jul-{end}",
            };

        public IDictionary<string, string> BuildFcsDeliverableCodeNameLookup(ICollection<FCSDeliverableCodeMapping> fcsDeliverableCodeMappings)
            => fcsDeliverableCodeMappings.ToDictionary(m => m.ExternalDeliverableCode, m => m.DeliverableName, StringComparer.OrdinalIgnoreCase);

        public IDictionary<string, LARSLearningDelivery> BuildLarsLearningDeliveryLookup(ICollection<LARSLearningDelivery> larsLearningDeliveries)
            => larsLearningDeliveries.ToDictionary(ld => ld.LearnAimRef, ld => ld, StringComparer.OrdinalIgnoreCase);

        public IDictionary<DPOutcomeKey, DPOutcome> BuildDpOutcomeLookup(ICollection<DPOutcome> dpOutcomes)
            => dpOutcomes.ToDictionary(dpo => new DPOutcomeKey(dpo.LearnRefNumber, dpo.OutcomeType, dpo.OutcomeCode, dpo.OutcomeStartDate), dpo => dpo);

        public IDictionary<DPOutcomeKey, ESFDPOutcome> BuildESFDpOutcomeLookup(ICollection<ESFDPOutcome> dpOutcomes)
            => dpOutcomes.ToDictionary(dpo => new DPOutcomeKey(dpo.LearnRefNumber, dpo.OutcomeType, dpo.OutcomeCode, dpo.OutcomeStartDate), dpo => dpo);

        public IDictionary<ESFLearningDeliveryDeliverableKey, ICollection<ESFLearningDeliveryDeliverablePeriod>> BuildEsfLearningDeliveryDeliverableLookup(ICollection<ESFLearningDeliveryDeliverablePeriod> esfLearningDeliveryDeliverablePeriods)
            => esfLearningDeliveryDeliverablePeriods
                .GroupBy(dp => new ESFLearningDeliveryDeliverableKey(dp.LearnRefNumber, dp.AimSequenceNumber), ESFLearningDeliveryDeliverableKey.Comparer)
                .ToDictionary(g => g.Key, g => g.ToList() as ICollection<ESFLearningDeliveryDeliverablePeriod>, ESFLearningDeliveryDeliverableKey.Comparer);

        public DPOutcomeKey BuildDpOutcomeKeyForLearningDelivery(LearningDelivery learningDelivery)
            => new DPOutcomeKey(
                learningDelivery.LearnRefNumber,
                learningDelivery.EligibleProgressionOutcomeType,
                learningDelivery.EligibleProgressionOutcomeCode.Value,
                learningDelivery.EligibleProgressionOutomeStartDate.Value);

        // BR4
        public bool IsRoundTwoContract(string conRefNumber)
        {
            if (!string.IsNullOrWhiteSpace(conRefNumber))
            {
                if (conRefNumber.StartsWith(EsfContractPrefix, StringComparison.OrdinalIgnoreCase))
                {
                    if (int.TryParse(conRefNumber.Substring(EsfContractPrefix.Length), out var id))
                    {
                        return id >= RoundTwoContractStartId;
                    }
                }
            }

            return false;
        }

        public bool IsDpOutcomeKey(LearningDelivery learningDelivery)
            => learningDelivery.LearnRefNumber != null
               && learningDelivery.EligibleProgressionOutcomeCode.HasValue
               && learningDelivery.EligibleProgressionOutcomeType != null
               && learningDelivery.EligibleProgressionOutomeStartDate.HasValue;

        private AimAndDeliverableReportRow BuildRow(
            LearningDelivery learningDelivery,
            DPOutcome dpOutcome,
            ESFDPOutcome esfDpOutcome,
            LARSLearningDelivery larsLearningDelivery,
            ESFLearningDeliveryDeliverablePeriod deliverablePeriod = null,
            string deliverableName = null,
            string reportMonth = null)
            => new AimAndDeliverableReportRow()
            {
                LearningDelivery = learningDelivery,
                DPOutcome = dpOutcome,
                ESFDPOutcome = esfDpOutcome,
                LarsLearningDelivery = larsLearningDelivery,
                DeliverablePeriod = deliverablePeriod,
                DeliverableName = deliverableName,
                ReportMonth = reportMonth,
            };
    }
}
