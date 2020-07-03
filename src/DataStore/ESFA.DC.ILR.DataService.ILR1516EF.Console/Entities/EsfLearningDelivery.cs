using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class EsfLearningDelivery
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public bool? Achieved { get; set; }
        public bool? AddProgCostElig { get; set; }
        public decimal? AdjustedAreaCostFactor { get; set; }
        public decimal? AdjustedPremiumFactor { get; set; }
        public DateTime? AdjustedStartDate { get; set; }
        public string AimClassification { get; set; }
        public decimal? AimValue { get; set; }
        public decimal? ApplicWeightFundRate { get; set; }
        public long? EligibleProgressionOutcomeCode { get; set; }
        public string EligibleProgressionOutcomeType { get; set; }
        public DateTime? EligibleProgressionOutomeStartDate { get; set; }
        public bool? FundStart { get; set; }
        public decimal? LarsweightedRate { get; set; }
        public DateTime? LatestPossibleStartDate { get; set; }
        public DateTime? LdesfengagementStartDate { get; set; }
        public bool? PotentiallyEligibleForProgression { get; set; }
        public DateTime? ProgressionEndDate { get; set; }
        public bool? Restart { get; set; }
        public decimal? WeightedRateFromEsol { get; set; }
    }
}
