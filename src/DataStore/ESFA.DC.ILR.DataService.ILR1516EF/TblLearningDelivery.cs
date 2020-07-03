using System;

namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class TblLearningDelivery
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public DateTime? AchApplicDate { get; set; }
        public bool? AchEligible { get; set; }
        public bool? Achieved { get; set; }
        public decimal? AchievementApplicVal { get; set; }
        public int? ActualDaysIl { get; set; }
        public int? ActualNumInstalm { get; set; }
        public DateTime? AdjProgStartDate { get; set; }
        public DateTime? AdjStartDate { get; set; }
        public int? AgeStandardStart { get; set; }
        public DateTime? ApplicFundValDate { get; set; }
        public decimal? CombinedAdjProp { get; set; }
        public decimal? CoreGovContCapApplicVal { get; set; }
        public int? EmpIdAchDate { get; set; }
        public int? EmpIdFirstDayStandard { get; set; }
        public int? EmpIdFirstYoungAppDate { get; set; }
        public int? EmpIdSecondYoungAppDate { get; set; }
        public int? EmpIdSmallBusDate { get; set; }
        public string FundLine { get; set; }
        public string LearnAimRef { get; set; }
        public int? LearnDelDaysIl { get; set; }
        public int? LearnDelStandardAccDaysIl { get; set; }
        public int? LearnDelStandardPrevAccDaysIl { get; set; }
        public int? LearnDelStandardTotalDaysIl { get; set; }
        public decimal? MathEngAimValue { get; set; }
        public bool? MathEngLsffundStart { get; set; }
        public int? MathEngLsfthresholdDays { get; set; }
        public int? OutstandNumOnProgInstalm { get; set; }
        public int? PlannedNumOnProgInstalm { get; set; }
        public int? PlannedTotalDaysIl { get; set; }
        public DateTime? ProgStandardStartDate { get; set; }
        public decimal? SmallBusApplicVal { get; set; }
        public bool? SmallBusEligible { get; set; }
        public int? SmallBusStatusFirstDayStandard { get; set; }
        public int? SmallBusStatusThreshold { get; set; }
        public DateTime? SmallBusThresholdDate { get; set; }
        public decimal? YoungAppApplicVal { get; set; }
        public bool? YoungAppEligible { get; set; }
        public DateTime? YoungAppFirstThresholdDate { get; set; }
        public DateTime? YoungAppSecondThresholdDate { get; set; }
    }
}
