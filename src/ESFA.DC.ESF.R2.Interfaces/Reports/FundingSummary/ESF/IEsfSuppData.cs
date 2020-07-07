using System;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary.ESF
{
    public interface IEsfSuppData
    {
        string ConRefNumber { get; }

        string DeliverableCode { get; }

        int? CalendarYear { get; }

        int? CalendarMonth { get; }

        string CostType { get; }

        string ReferenceType { get; }

        string Reference { get; }

        long? ULN { get; }

        string ProviderSpecifiedReference { get; }

        decimal? Value { get; }

        string LearnAimRef { get; }

        DateTime? SupplementaryDataPanelDate { get; }
    }
}
