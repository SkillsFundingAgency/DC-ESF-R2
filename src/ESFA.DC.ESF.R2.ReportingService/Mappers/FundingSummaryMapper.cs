using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.Mappers
{
    public sealed class FundingSummaryMapper : ClassMap<FundingSummaryModel>, IClassMapper
    {
        private const string NotApplicable = "N/A";

        public FundingSummaryMapper()
        {
            Map(m => m.Title).Index(0).Name("NA");
            Map(m => m.YearlyValues).Index(1).Name("August {Y}", "September {Y}", "October {Y}", "November {Y}", "December {Y}", "January {Y}", "February {Y}", "March {Y}", "April {Y}", "May {Y}", "June {Y}", "July {Y}").TypeConverterOption.Format("0.00").TypeConverterOption.NullValues(NotApplicable);
            Map(m => m.Totals).Index(2).Name("{SP}/{SY} Subtotal").TypeConverterOption.Format("0.00").TypeConverterOption.NullValues(NotApplicable);
            Map(m => m.GrandTotal).Index(3).Name("Grand Total").TypeConverterOption.Format("0.00").TypeConverterOption.NullValues(NotApplicable);
        }
    }
}
