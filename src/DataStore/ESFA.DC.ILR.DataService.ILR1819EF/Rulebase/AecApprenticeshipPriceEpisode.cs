using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
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
        public decimal? PriceEpisodeUpperBandLimit { get; set; }
        public DateTime? PriceEpisodePlannedEndDate { get; set; }
        public DateTime? PriceEpisodeActualEndDate { get; set; }
        public decimal? PriceEpisodeTotalTnpprice { get; set; }
        public decimal? PriceEpisodeUpperLimitAdjustment { get; set; }
        public int? PriceEpisodePlannedInstalments { get; set; }
        public int? PriceEpisodeActualInstalments { get; set; }
        public int? PriceEpisodeInstalmentsThisPeriod { get; set; }
        public decimal? PriceEpisodeCompletionElement { get; set; }
        public decimal? PriceEpisodePreviousEarnings { get; set; }
        public decimal? PriceEpisodeInstalmentValue { get; set; }
        public decimal? PriceEpisodeOnProgPayment { get; set; }
        public decimal? PriceEpisodeTotalEarnings { get; set; }
        public decimal? PriceEpisodeBalanceValue { get; set; }
        public decimal? PriceEpisodeBalancePayment { get; set; }
        public bool? PriceEpisodeCompleted { get; set; }
        public decimal? PriceEpisodeCompletionPayment { get; set; }
        public decimal? PriceEpisodeRemainingTnpamount { get; set; }
        public decimal? PriceEpisodeRemainingAmountWithinUpperLimit { get; set; }
        public decimal? PriceEpisodeCappedRemainingTnpamount { get; set; }
        public decimal? PriceEpisodeExpectedTotalMonthlyValue { get; set; }
        public long? PriceEpisodeAimSeqNumber { get; set; }
        public decimal? PriceEpisodeFirstDisadvantagePayment { get; set; }
        public decimal? PriceEpisodeSecondDisadvantagePayment { get; set; }
        public decimal? PriceEpisodeApplic1618FrameworkUpliftBalancing { get; set; }
        public decimal? PriceEpisodeApplic1618FrameworkUpliftCompletionPayment { get; set; }
        public decimal? PriceEpisodeApplic1618FrameworkUpliftOnProgPayment { get; set; }
        public decimal? PriceEpisodeSecondProv1618Pay { get; set; }
        public decimal? PriceEpisodeFirstEmp1618Pay { get; set; }
        public decimal? PriceEpisodeSecondEmp1618Pay { get; set; }
        public decimal? PriceEpisodeFirstProv1618Pay { get; set; }
        public decimal? PriceEpisodeLsfcash { get; set; }
        public string PriceEpisodeFundLineType { get; set; }
        public decimal? PriceEpisodeSfacontribPct { get; set; }
        public int? PriceEpisodeLevyNonPayInd { get; set; }
        public DateTime? EpisodeEffectiveTnpstartDate { get; set; }
        public DateTime? PriceEpisodeFirstAdditionalPaymentThresholdDate { get; set; }
        public DateTime? PriceEpisodeSecondAdditionalPaymentThresholdDate { get; set; }
        public string PriceEpisodeContractType { get; set; }
        public decimal? PriceEpisodePreviousEarningsSameProvider { get; set; }
        public decimal? PriceEpisodeTotProgFunding { get; set; }
        public decimal? PriceEpisodeProgFundIndMinCoInvest { get; set; }
        public decimal? PriceEpisodeProgFundIndMaxEmpCont { get; set; }
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
