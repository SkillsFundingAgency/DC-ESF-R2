﻿using System;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class SupplementaryData
    {
        public int SupplementaryDataId { get; set; }
        public string ConRefNumber { get; set; }
        public string DeliverableCode { get; set; }
        public int CalendarYear { get; set; }
        public int CalendarMonth { get; set; }
        public string CostType { get; set; }
        public string ReferenceType { get; set; }
        public string Reference { get; set; }
        public long? ULN { get; set; }
        public string ProviderSpecifiedReference { get; set; }
        public decimal? Value { get; set; }
        public string LearnAimRef { get; set; }
        public DateTime? SupplementaryDataPanelDate { get; set; }
        public int SourceFileId { get; set; }

        public virtual SourceFile SourceFile { get; set; }
        public virtual SupplementaryDataUnitCost SupplementaryDataUnitCost { get; set; }
    }
}
