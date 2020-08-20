﻿namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
     public interface IPeriodisedReportValue
     {
        string Title { get; }

        decimal[] MonthlyValues { get; set; }

        decimal Total { get; }
    }
}
