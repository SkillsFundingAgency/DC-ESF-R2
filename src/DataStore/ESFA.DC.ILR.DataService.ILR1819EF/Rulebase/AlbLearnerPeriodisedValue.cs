﻿namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class AlbLearnerPeriodisedValue
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string AttributeName { get; set; }
        public decimal? Period1 { get; set; }
        public decimal? Period2 { get; set; }
        public decimal? Period3 { get; set; }
        public decimal? Period4 { get; set; }
        public decimal? Period5 { get; set; }
        public decimal? Period6 { get; set; }
        public decimal? Period7 { get; set; }
        public decimal? Period8 { get; set; }
        public decimal? Period9 { get; set; }
        public decimal? Period10 { get; set; }
        public decimal? Period11 { get; set; }
        public decimal? Period12 { get; set; }

        public virtual AlbLearner AlbLearner { get; set; }
    }
}
