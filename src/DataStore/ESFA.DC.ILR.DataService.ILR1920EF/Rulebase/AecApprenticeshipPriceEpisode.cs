using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Rulebase
{
    public partial class AecApprenticeshipPriceEpisode
    {
        public AecApprenticeshipPriceEpisode()
        {
            AecApprenticeshipPriceEpisodePeriodisedValues = new HashSet<AecApprenticeshipPriceEpisodePeriodisedValue>();
            AecApprenticeshipPriceEpisodePeriods = new HashSet<AecApprenticeshipPriceEpisodePeriod>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public decimal? Tnp4 { get; set; }
        public decimal? Tnp1 { get; set; }
        public DateTime? EpisodeStartDate { get; set; }
        public decimal? Tnp2 { get; set; }
        public decimal? Tnp3 { get; set; }
        public decimal? PriceEpisode1618FrameworkUpliftRemainingAmount { get; set; }
        public decimal? PriceEpisode1618FrameworkUpliftTotPrevEarnings { get; set; }
        public decimal? PriceEpisode1618FubalValue { get; set; }
        public decimal? PriceEpisode1618FumonthInstValue { get; set; }
        public decimal? PriceEpisode1618FutotEarnings { get; set; }
        public decimal? PriceEpisodeUpperBandLimit { get; set; }
        public DateTime? PriceEpisodePlannedEndDate { get; set; }
        public DateTime? PriceEpisodeActualEndDate { get; set; }
        public DateTime? PriceEpisodeActualEndDateIncEpa { get; set; }
        public decimal? PriceEpisodeTotalTnpprice { get; set; }
        public decimal? PriceEpisodeUpperLimitAdjustment { get; set; }
        public int? PriceEpisodePlannedInstalments { get; set; }
        public int? PriceEpisodeActualInstalments { get; set; }
        public decimal? PriceEpisodeCompletionElement { get; set; }
        public decimal? PriceEpisodePreviousEarnings { get; set; }
        public decimal? PriceEpisodeInstalmentValue { get; set; }
        public decimal? PriceEpisodeTotalEarnings { get; set; }
        public bool? PriceEpisodeCompleted { get; set; }
        public decimal? PriceEpisodeRemainingTnpamount { get; set; }
        public decimal? PriceEpisodeRemainingAmountWithinUpperLimit { get; set; }
        public decimal? PriceEpisodeCappedRemainingTnpamount { get; set; }
        public decimal? PriceEpisodeExpectedTotalMonthlyValue { get; set; }
        public int? PriceEpisodeAimSeqNumber { get; set; }
        public decimal? PriceEpisodeApplic1618FrameworkUpliftCompElement { get; set; }
        public string PriceEpisodeFundLineType { get; set; }
        public DateTime? EpisodeEffectiveTnpstartDate { get; set; }
        public DateTime? PriceEpisodeFirstAdditionalPaymentThresholdDate { get; set; }
        public DateTime? PriceEpisodeSecondAdditionalPaymentThresholdDate { get; set; }
        public string PriceEpisodeContractType { get; set; }
        public decimal? PriceEpisodePreviousEarningsSameProvider { get; set; }
        public decimal? PriceEpisodeTotalPmrs { get; set; }
        public decimal? PriceEpisodeCumulativePmrs { get; set; }
        public int? PriceEpisodeCompExemCode { get; set; }
        public DateTime? PriceEpisodeLearnerAdditionalPaymentThresholdDate { get; set; }
        public string PriceEpisodeAgreeId { get; set; }
        public DateTime? PriceEpisodeRedStartDate { get; set; }
        public int? PriceEpisodeRedStatusCode { get; set; }

        public virtual AecLearner AecLearner { get; set; }
        public virtual ICollection<AecApprenticeshipPriceEpisodePeriodisedValue> AecApprenticeshipPriceEpisodePeriodisedValues { get; set; }
        public virtual ICollection<AecApprenticeshipPriceEpisodePeriod> AecApprenticeshipPriceEpisodePeriods { get; set; }
    }
}
