using System;

namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class AecApprenticeshipPriceEpisode
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string PriceEpisodeIdentifier { get; set; }
        public DateTime? EpisodeEffectiveTnpstartDate { get; set; }
        public DateTime? EpisodeStartDate { get; set; }
        public DateTime? PriceEpisodeActualEndDate { get; set; }
        public int? PriceEpisodeActualInstalments { get; set; }
        public int? PriceEpisodeAimSeqNumber { get; set; }
        public decimal? PriceEpisodeCappedRemainingTnpamount { get; set; }
        public bool? PriceEpisodeCompleted { get; set; }
        public decimal? PriceEpisodeCompletionElement { get; set; }
        public decimal? PriceEpisodeExpectedTotalMonthlyValue { get; set; }
        public DateTime? PriceEpisodeFirstAdditionalPaymentThresholdDate { get; set; }
        public decimal? PriceEpisodeInstalmentValue { get; set; }
        public DateTime? PriceEpisodePlannedEndDate { get; set; }
        public int? PriceEpisodePlannedInstalments { get; set; }
        public decimal? PriceEpisodePreviousEarnings { get; set; }
        public decimal? PriceEpisodeRemainingAmountWithinUpperLimit { get; set; }
        public decimal? PriceEpisodeRemainingTnpamount { get; set; }
        public DateTime? PriceEpisodeSecondAdditionalPaymentThresholdDate { get; set; }
        public decimal? PriceEpisodeTotalEarnings { get; set; }
        public decimal? PriceEpisodeTotalTnpprice { get; set; }
        public decimal? PriceEpisodeUpperBandLimit { get; set; }
        public decimal? PriceEpisodeUpperLimitAdjustment { get; set; }
        public decimal? Tnp1 { get; set; }
        public decimal? Tnp2 { get; set; }
        public decimal? Tnp3 { get; set; }
        public decimal? Tnp4 { get; set; }
        public string PriceEpisodeContractType { get; set; }
        public string PriceEpisodeFundLineType { get; set; }
        public decimal? PriceEpisodePreviousEarningsSameProvider { get; set; }
    }
}
