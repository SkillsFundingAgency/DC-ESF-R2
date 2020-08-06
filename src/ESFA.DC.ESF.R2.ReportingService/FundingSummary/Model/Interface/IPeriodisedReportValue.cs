namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
     public interface IPeriodisedReportValue
     {
         string Title { get; }

         decimal? April { get; }

         decimal? May { get; }

         decimal? June { get; }

         decimal? July { get; }

         decimal? August { get; }

         decimal? September { get; }

         decimal? October { get; }

         decimal? November { get; }

         decimal? December { get; }

         decimal? January { get; }

         decimal? February { get; }

         decimal? March { get; }

         decimal? Total { get; }
    }
}
