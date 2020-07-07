using System;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary.ESF;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ESF
{
    public class EsfSuppData : IEsfSuppData
    {
        public string ConRefNumber { get; set; }

        public string DeliverableCode { get; set; }

        public int? CalendarYear { get; set; }

        public int? CalendarMonth { get; set; }

        public string CostType { get; set; }

        public string ReferenceType { get; set; }

        public string Reference { get; set; }

        public long? ULN { get; set; }

        public string ProviderSpecifiedReference { get; set; }

        public decimal? Value { get; set; }

        public string LearnAimRef { get; set; }

        public DateTime? SupplementaryDataPanelDate { get; set; }
    }
}
