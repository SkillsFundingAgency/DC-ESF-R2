using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class AlbLearningDelivery
    {
        public AlbLearningDelivery()
        {
            AlbLearningDeliveryPeriodisedValues = new HashSet<AlbLearningDeliveryPeriodisedValue>();
            AlbLearningDeliveryPeriods = new HashSet<AlbLearningDeliveryPeriod>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public decimal? AreaCostFactAdj { get; set; }
        public decimal? WeightedRate { get; set; }
        public int? PlannedNumOnProgInstalm { get; set; }
        public bool? FundStart { get; set; }
        public bool? Achieved { get; set; }
        public int? ActualNumInstalm { get; set; }
        public int? OutstndNumOnProgInstalm { get; set; }
        public decimal? AreaCostInstalment { get; set; }
        public bool? AdvLoan { get; set; }
        public bool? LoanBursAreaUplift { get; set; }
        public bool? LoanBursSupp { get; set; }
        public string FundLine { get; set; }
        public DateTime? LiabilityDate { get; set; }
        public string ApplicProgWeightFact { get; set; }
        public DateTime? ApplicFactDate { get; set; }

        public virtual AlbLearner AlbLearner { get; set; }
        public virtual ICollection<AlbLearningDeliveryPeriodisedValue> AlbLearningDeliveryPeriodisedValues { get; set; }
        public virtual ICollection<AlbLearningDeliveryPeriod> AlbLearningDeliveryPeriods { get; set; }
    }
}
