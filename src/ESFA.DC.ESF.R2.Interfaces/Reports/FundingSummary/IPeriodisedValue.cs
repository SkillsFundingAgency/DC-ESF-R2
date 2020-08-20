namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary
{
    public interface IPeriodisedValue
    {
        string ConRefNumber { get; }

        string DeliverableCode { get; }

        string AttributeName { get; }

        decimal[] MonthlyValues { get; }
    }
}
