using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ILR
{
    public class PeriodisedValue : IPeriodisedValue
    {
        public int UKPRN { get; set; }

        public string LearnRefNumber { get; set; }

        public int AimSeqNumber { get; set; }

        public string DeliverableCode { get; set; }

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

        public string ConRefNumber { get; set; }
    }
}
