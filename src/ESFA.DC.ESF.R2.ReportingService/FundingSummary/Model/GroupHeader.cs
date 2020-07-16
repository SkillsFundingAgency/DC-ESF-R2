namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class GroupHeader
    {
        public GroupHeader(
            string title,
            string headerApril,
            string headerMay,
            string headerJune,
            string headerJuly,
            string headerAugust,
            string headerSeptember,
            string headerOctober,
            string headerNovember,
            string headerDecember,
            string headerJanuary,
            string headerFebruary,
            string headerMarch)
        {
            Title = title;
            HeaderApril = headerApril;
            HeaderMay = headerMay;
            HeaderJune = headerJune;
            HeaderJuly = headerJuly;
            HeaderAugust = headerAugust;
            HeaderSeptember = headerSeptember;
            HeaderOctober = headerOctober;
            HeaderNovember = headerNovember;
            HeaderDecember = headerDecember;
            HeaderJanuary = headerJanuary;
            HeaderFebruary = headerFebruary;
            HeaderMarch = headerMarch;
        }

        public string Title { get; set; }

        public string HeaderApril { get; set; }

        public string HeaderMay { get; set; }

        public string HeaderJune { get; set; }

        public string HeaderJuly { get; set; }

        public string HeaderAugust { get; set; }

        public string HeaderSeptember { get; set; }

        public string HeaderOctober { get; set; }

        public string HeaderNovember { get; set; }

        public string HeaderDecember { get; set; }

        public string HeaderJanuary { get; set; }

        public string HeaderFebruary { get; set; }

        public string HeaderMarch { get; set; }

        public decimal Total { get; set; }
    }
}
