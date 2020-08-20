using System.Collections.Generic;
using ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model
{
    public class FundingSummaryReportEarnings
    {
        public int Year { get; set; }

        public string AcademicYear { get; set; }

        public string ConRefNumber { get; set; }

        public PeriodisedReportValue MonthlyTotals { get; set; }

        public PeriodisedReportValue CumulativeMonthlyTotals { get; set; }

        public ICollection<IDeliverableCategory> DeliverableCategories { get; set; }

        public decimal? PreviousYearCumulativeTotal { get; set; }

        public decimal? YearTotal { get; set; }

        public decimal? CumulativeYearTotal { get; set; }
    }
}