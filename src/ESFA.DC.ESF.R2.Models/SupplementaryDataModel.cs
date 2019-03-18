using System;

namespace ESFA.DC.ESF.R2.Models
{
    public class SupplementaryDataModel
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

        public string OfficialSensitive { get; }
    }
}
