using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class Fm70LearningDeliveryModel
    {
        public int UkPrn { get; set; }

        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string DeliverableCode { get; set; }

        public decimal? ApplicWeightFundRate { get; set; }

        public decimal? AimValue { get; set; }

        public decimal? AdjustedAreaCostFactor { get; set; }

        public decimal? AdjustedPremiumFactor { get; set; }

        public DateTime? LdEsfEngagementStartDate { get; set; }

        public DateTime? LatestPossibleStartDate { get; set; }

        public DateTime? EligibleProgressionOutomeStartDate { get; set; }

        public string EligibleProgressionOutcomeType { get; set; }

        public long? EligibleProgressionOutcomeCode { get; set; }

        public IEnumerable<Fm70LearningDeliveryDeliverableModel> Fm70LearningDeliveryDeliverables { get; set; }

        public IEnumerable<Fm70LearningDeliveryDeliverablePeriodModel> Fm70LearningDeliveryDeliverablePeriods { get; set; }
    }
}