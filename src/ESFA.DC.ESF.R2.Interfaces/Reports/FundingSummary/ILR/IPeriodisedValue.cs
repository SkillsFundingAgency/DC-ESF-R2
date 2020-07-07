namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR
{
    public interface IPeriodisedValue
    {
        int UKPRN { get; }

        string LearnRefNumber { get; }

        int AimSeqNumber { get; }

        string DeliverableCode { get; }

        string AttributeName { get; }

        decimal? Period1 { get; }

        decimal? Period2 { get; }

        decimal? Period3 { get; }

        decimal? Period4 { get; }

        decimal? Period5 { get; }

        decimal? Period6 { get; }

        decimal? Period7 { get; }

        decimal? Period8 { get; }

        decimal? Period9 { get; }

        decimal? Period10 { get; }

        decimal? Period11 { get; }

        decimal? Period12 { get; }

        string ConRefNumber { get; }
    }
}
