using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class AlbLearningDelivery
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public bool? Achieved { get; set; }
        public int? ActualNumInstalm { get; set; }
        public bool? AdvLoan { get; set; }
        public DateTime? AlbpaymentEndDate { get; set; }
        public string ApplicProgWeightFact { get; set; }
        public decimal? AreaCostFactAdj { get; set; }
        public decimal? AreaCostInstalment { get; set; }
        public string CourseType { get; set; }
        public string FundLine { get; set; }
        public bool? FundStart { get; set; }
        public DateTime? LiabilityDate { get; set; }
        public bool? LoanBursAreaUplift { get; set; }
        public bool? LoanBursSupp { get; set; }
        public int? MaxNumYears { get; set; }
        public int? OutstndNumOnProgInstalm { get; set; }
        public int? PlannedNumOnProgInstalm { get; set; }
        public decimal? SpecResUplift { get; set; }
        public decimal? WeightedRate { get; set; }
    }
}
