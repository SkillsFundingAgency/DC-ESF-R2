using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class Fm35LearningDelivery
    {
        public Fm35LearningDelivery()
        {
            Fm35LearningDeliveryPeriodisedValues = new HashSet<Fm35LearningDeliveryPeriodisedValue>();
            Fm35LearningDeliveryPeriods = new HashSet<Fm35LearningDeliveryPeriod>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public DateTime? AchApplicDate { get; set; }
        public bool? Achieved { get; set; }
        public decimal? AchieveElement { get; set; }
        public bool? AchievePayElig { get; set; }
        public decimal? AchievePayPctPreTrans { get; set; }
        public decimal? AchPayTransHeldBack { get; set; }
        public int? ActualDaysIl { get; set; }
        public int? ActualNumInstalm { get; set; }
        public int? ActualNumInstalmPreTrans { get; set; }
        public int? ActualNumInstalmTrans { get; set; }
        public DateTime? AdjLearnStartDate { get; set; }
        public bool? AdltLearnResp { get; set; }
        public int? AgeAimStart { get; set; }
        public decimal? AimValue { get; set; }
        public DateTime? AppAdjLearnStartDate { get; set; }
        public decimal? AppAgeFact { get; set; }
        public bool? AppAtagta { get; set; }
        public bool? AppCompetency { get; set; }
        public bool? AppFuncSkill { get; set; }
        public decimal? AppFuncSkill1618AdjFact { get; set; }
        public bool? AppKnowl { get; set; }
        public DateTime? AppLearnStartDate { get; set; }
        public DateTime? ApplicEmpFactDate { get; set; }
        public DateTime? ApplicFactDate { get; set; }
        public DateTime? ApplicFundRateDate { get; set; }
        public string ApplicProgWeightFact { get; set; }
        public decimal? ApplicUnweightFundRate { get; set; }
        public decimal? ApplicWeightFundRate { get; set; }
        public bool? AppNonFund { get; set; }
        public decimal? AreaCostFactAdj { get; set; }
        public int? BalInstalmPreTrans { get; set; }
        public decimal? BaseValueUnweight { get; set; }
        public decimal? CapFactor { get; set; }
        public decimal? DisUpFactAdj { get; set; }
        public bool? EmpOutcomePayElig { get; set; }
        public decimal? EmpOutcomePctHeldBackTrans { get; set; }
        public decimal? EmpOutcomePctPreTrans { get; set; }
        public bool? EmpRespOth { get; set; }
        public bool? Esol { get; set; }
        public bool? FullyFund { get; set; }
        public string FundLine { get; set; }
        public bool? FundStart { get; set; }
        public int? LargeEmployerId { get; set; }
        public decimal? LargeEmployerFm35fctr { get; set; }
        public DateTime? LargeEmployerStatusDate { get; set; }
        public decimal? LtrcupliftFctr { get; set; }
        public decimal? NonGovCont { get; set; }
        public bool? Olasscustody { get; set; }
        public decimal? OnProgPayPctPreTrans { get; set; }
        public int? OutstndNumOnProgInstalm { get; set; }
        public int? OutstndNumOnProgInstalmTrans { get; set; }
        public int? PlannedNumOnProgInstalm { get; set; }
        public int? PlannedNumOnProgInstalmTrans { get; set; }
        public int? PlannedTotalDaysIl { get; set; }
        public int? PlannedTotalDaysIlpreTrans { get; set; }
        public decimal? PropFundRemain { get; set; }
        public decimal? PropFundRemainAch { get; set; }
        public bool? PrscHeaim { get; set; }
        public bool? Residential { get; set; }
        public bool? Restart { get; set; }
        public decimal? SpecResUplift { get; set; }
        public decimal? StartPropTrans { get; set; }
        public int? ThresholdDays { get; set; }
        public bool? Traineeship { get; set; }
        public bool? Trans { get; set; }
        public DateTime? TrnAdjLearnStartDate { get; set; }
        public bool? TrnWorkPlaceAim { get; set; }
        public bool? TrnWorkPrepAim { get; set; }
        public decimal? UnWeightedRateFromEsol { get; set; }
        public decimal? UnweightedRateFromLars { get; set; }
        public decimal? WeightedRateFromEsol { get; set; }
        public decimal? WeightedRateFromLars { get; set; }

        public virtual Fm35Learner Fm35Learner { get; set; }
        public virtual ICollection<Fm35LearningDeliveryPeriodisedValue> Fm35LearningDeliveryPeriodisedValues { get; set; }
        public virtual ICollection<Fm35LearningDeliveryPeriod> Fm35LearningDeliveryPeriods { get; set; }
    }
}
