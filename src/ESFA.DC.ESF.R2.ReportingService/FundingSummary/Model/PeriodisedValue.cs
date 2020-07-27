using ESFA.DC.ESF.R2.Interfaces.FundingSummary;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class PeriodisedValue : IPeriodisedValue
    {
        public PeriodisedValue(
            string conRefNumber,
            string deliverableCode,
            string attributeName,
            decimal? august,
            decimal? september,
            decimal? october,
            decimal? november,
            decimal? december,
            decimal? january,
            decimal? february,
            decimal? march,
            decimal? april,
            decimal? may,
            decimal? june,
            decimal? july)
        {
            ConRefNumber = conRefNumber;
            DeliverableCode = deliverableCode;
            AttributeName = attributeName;
            April = april;
            May = may;
            June = june;
            July = july;
            August = august;
            September = september;
            October = october;
            Novemeber = november;
            December = december;
            January = january;
            February = february;
            March = march;
    }

        public string ConRefNumber { get; set; }

        public string DeliverableCode { get; set; }

        public string AttributeName { get; set; }

        public decimal? April { get; set; }

        public decimal? May { get; set; }

        public decimal? June { get; set; }

        public decimal? July { get; set; }

        public decimal? August { get; set; }

        public decimal? September { get; set; }

        public decimal? October { get; set; }

        public decimal? Novemeber { get; set; }

        public decimal? December { get; set; }

        public decimal? January { get; set; }

        public decimal? February { get; set; }

        public decimal? March { get; set; }
    }
}
