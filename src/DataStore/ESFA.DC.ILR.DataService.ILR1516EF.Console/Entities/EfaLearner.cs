using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class EfaLearner
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public decimal RetentNew { get; set; }
        public int? AcadMonthPayment { get; set; }
        public bool? AcadProg { get; set; }
        public int? ActualDaysIlcurrYear { get; set; }
        public decimal? AreaCostFact1618Hist { get; set; }
        public decimal? Block1DisadvUpliftNew { get; set; }
        public decimal? Block2DisadvElementsNew { get; set; }
        public string ConditionOfFundingEnglish { get; set; }
        public string ConditionOfFundingMaths { get; set; }
        public int? CoreAimSeqNumber { get; set; }
        public decimal? FullTimeEquiv { get; set; }
        public string FundLine { get; set; }
        public DateTime? LearnerActEndDate { get; set; }
        public DateTime? LearnerPlanEndDate { get; set; }
        public DateTime? LearnerStartDate { get; set; }
        public decimal? NatRate { get; set; }
        public decimal? OnProgPayment { get; set; }
        public int? PlannedDaysIlcurrYear { get; set; }
        public decimal? ProgWeightHist { get; set; }
        public decimal? ProgWeightNew { get; set; }
        public decimal? PrvDisadvPropnHist { get; set; }
        public decimal? PrvRetentFactHist { get; set; }
        public string RateBand { get; set; }
        public bool? StartFund { get; set; }
        public int? ThresholdDays { get; set; }
    }
}
