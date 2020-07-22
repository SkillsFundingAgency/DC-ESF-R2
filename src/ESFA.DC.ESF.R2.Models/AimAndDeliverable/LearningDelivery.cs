using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ESF.R2.Models.AimAndDeliverable
{
    public class LearningDelivery
    {
        // Learner Denormalised
        public long ULN { get; set; }

        public string LearnRefNumber { get; set; }

        public string GivenNames { get; set; }

        public string FamilyName { get; set; }

        public string CampId { get; set; }

        public int? PMUKPRN { get; set; }

        // Provider Specified Learner Monitorings Denormalised
        public string ProviderSpecLearnerMonitoring_A { get; set; }

        public string ProviderSpecLearnerMonitoring_B { get; set; }

        //Learning Delivery
        public string ConRefNumber { get; set; }

        public string LearnAimRef { get; set; }

        public int AimSeqNumber { get; set; }

        public int FundModel { get; set; }

        public DateTime LearnStartDate { get; set; }

        public DateTime LearnPlanEndDate { get; set; }

        public DateTime? LearnActEndDate { get; set; }

        public int CompStatus { get; set; }

        public string DelLocPostCode { get; set; }

        public int? Outcome { get; set; }

        public int? AddHours { get; set; }

        public int? PartnerUKPRN { get; set; }

        public string SWSupAimId { get; set; }

        // Learning Delivery FAM Denormalised
        public string LearningDeliveryFAM_RES { get; set; }

        // Provider Specified Delivery Monitoring Denormalised
        public string ProviderSpecDeliveryMonitoring_A { get; set; }

        public string ProviderSpecDeliveryMonitoring_B { get; set; }

        public string ProviderSpecDeliveryMonitoring_C { get; set; }

        public string ProviderSpecDeliveryMonitoring_D { get; set; }

        // ESF Learning Delivery
        public decimal? ApplicWeightFundRate { get; set; }

        public decimal? AimValue { get; set; }

        public decimal? AdjustedAreaCostFactor { get; set; }

        public decimal? AdjustedPremiumFactor { get; set; }

        public DateTime? LDESFEngagementStartDate { get; set; }

        public DateTime? LatestPossibleStartDate { get; set; }

        public DateTime? EligibleProgressionOutomeStartDate { get; set; }

        public string EligibleProgressionOutcomeType { get; set; }

        public long? EligibleProgressionOutcomeCode { get; set; }

        // ESF Learning Delivery Deliverable
        public string DeliverableCode { get; set; }
    }
}
