namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary
{
    public interface IPeriodisedValue
    {
        string ConRefNumber { get; }

        string DeliverableCode { get; }

        string AttributeName { get; }

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
    }
}
