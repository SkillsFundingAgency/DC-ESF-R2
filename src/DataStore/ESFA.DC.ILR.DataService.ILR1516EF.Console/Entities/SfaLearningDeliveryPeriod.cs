using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class SfaLearningDeliveryPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public int Period { get; set; }
        public decimal? AchievePayment { get; set; }
        public decimal? AchievePayPct { get; set; }
        public decimal? AchievePayPctTrans { get; set; }
        public decimal? BalancePayment { get; set; }
        public decimal? BalancePaymentUncapped { get; set; }
        public decimal? BalancePct { get; set; }
        public decimal? BalancePctTrans { get; set; }
        public decimal? EmpOutcomePay { get; set; }
        public decimal? EmpOutcomePct { get; set; }
        public decimal? EmpOutcomePctTrans { get; set; }
        public int? InstPerPeriod { get; set; }
        public bool? LearnSuppFund { get; set; }
        public decimal? LearnSuppFundCash { get; set; }
        public decimal? OnProgPayment { get; set; }
        public decimal? OnProgPaymentUncapped { get; set; }
        public decimal? OnProgPayPct { get; set; }
        public decimal? OnProgPayPctTrans { get; set; }
        public int? TransInstPerPeriod { get; set; }
    }
}
