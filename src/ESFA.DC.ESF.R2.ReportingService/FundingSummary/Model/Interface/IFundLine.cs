namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.Interface
{
    public interface IFundLine : IFundingSummaryReportRow
    {
        bool IncludeInTotals { get; }
    }
}
