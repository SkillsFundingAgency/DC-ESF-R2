namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class GroupHeader
    {
        public GroupHeader(
            string title,
            string headerAugust,
            string headerSeptember,
            string headerOctober,
            string headerNovember,
            string headerDecember,
            string headerJanuary,
            string headerFebruary,
            string headerMarch,
            string headerApril,
            string headerMay,
            string headerJune,
            string headerJuly)
        {
            Title = title;
            Months = new string[]
            {
                headerAugust,
                headerSeptember,
                headerOctober,
                headerNovember,
                headerDecember,
                headerJanuary,
                headerFebruary,
                headerMarch,
                headerApril,
                headerMay,
                headerJune,
                headerJuly
            };
        }

        public string Title { get; set; }

        public string[] Months { get; set; }
    }
}
