using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class AecLearningDelivery
    {
        public AecLearningDelivery()
        {
            AecLearningDeliveryPeriodisedTextValues = new HashSet<AecLearningDeliveryPeriodisedTextValue>();
            AecLearningDeliveryPeriodisedValues = new HashSet<AecLearningDeliveryPeriodisedValue>();
            AecLearningDeliveryPeriods = new HashSet<AecLearningDeliveryPeriod>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public int? ActualDaysIl { get; set; }
        public int? ActualNumInstalm { get; set; }
        public DateTime? AdjStartDate { get; set; }
        public int? AgeAtProgStart { get; set; }
        public DateTime? AppAdjLearnStartDate { get; set; }
        public DateTime? AppAdjLearnStartDateMatchPathway { get; set; }
        public DateTime? ApplicCompDate { get; set; }
        public decimal? CombinedAdjProp { get; set; }
        public bool? Completed { get; set; }
        public DateTime? FirstIncentiveThresholdDate { get; set; }
        public bool? FundStart { get; set; }
        public decimal? Ldapplic1618FrameworkUpliftTotalActEarnings { get; set; }
        public string LearnAimRef { get; set; }
        public bool? LearnDel1618AtStart { get; set; }
        public int? LearnDelAppAccDaysIl { get; set; }
        public decimal? LearnDelApplicDisadvAmount { get; set; }
        public decimal? LearnDelApplicEmp1618Incentive { get; set; }
        public DateTime? LearnDelApplicEmpDate { get; set; }
        public decimal? LearnDelApplicProv1618FrameworkUplift { get; set; }
        public decimal? LearnDelApplicProv1618Incentive { get; set; }
        public int? LearnDelAppPrevAccDaysIl { get; set; }
        public int? LearnDelDaysIl { get; set; }
        public decimal? LearnDelDisadAmount { get; set; }
        public bool? LearnDelEligDisadvPayment { get; set; }
        public int? LearnDelEmpIdFirstAdditionalPaymentThreshold { get; set; }
        public int? LearnDelEmpIdSecondAdditionalPaymentThreshold { get; set; }
        public int? LearnDelHistDaysThisApp { get; set; }
        public decimal? LearnDelHistProgEarnings { get; set; }
        public string LearnDelInitialFundLineType { get; set; }
        public bool? LearnDelMathEng { get; set; }
        public decimal? MathEngAimValue { get; set; }
        public int? OutstandNumOnProgInstalm { get; set; }
        public int? PlannedNumOnProgInstalm { get; set; }
        public int? PlannedTotalDaysIl { get; set; }
        public DateTime? SecondIncentiveThresholdDate { get; set; }
        public int? ThresholdDays { get; set; }
        public DateTime? LearnDelProgEarliestAct2date { get; set; }
        public bool? LearnDelNonLevyProcured { get; set; }
        public decimal? LearnDelApplicCareLeaverIncentive { get; set; }
        public int? LearnDelHistDaysCareLeavers { get; set; }
        public int? LearnDelAccDaysIlcareLeavers { get; set; }
        public int? LearnDelPrevAccDaysIlcareLeavers { get; set; }
        public DateTime? LearnDelLearnerAddPayThresholdDate { get; set; }
        public int? LearnDelRedCode { get; set; }
        public DateTime? LearnDelRedStartDate { get; set; }

        public virtual AecLearner AecLearner { get; set; }
        public virtual ICollection<AecLearningDeliveryPeriodisedTextValue> AecLearningDeliveryPeriodisedTextValues { get; set; }
        public virtual ICollection<AecLearningDeliveryPeriodisedValue> AecLearningDeliveryPeriodisedValues { get; set; }
        public virtual ICollection<AecLearningDeliveryPeriod> AecLearningDeliveryPeriods { get; set; }
    }
}
