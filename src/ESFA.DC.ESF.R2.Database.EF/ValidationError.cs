using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class ValidationError
    {
        public int SourceFileId { get; set; }
        public int ValidationError_Id { get; set; }
        public Guid? RowId { get; set; }
        public string RuleId { get; set; }
        public string ConRefNumber { get; set; }
        public string DeliverableCode { get; set; }
        public string CalendarYear { get; set; }
        public string CalendarMonth { get; set; }
        public string CostType { get; set; }
        public string ReferenceType { get; set; }
        public string Reference { get; set; }
        public string StaffName { get; set; }
        public string ULN { get; set; }
        public string Severity { get; set; }
        public string ErrorMessage { get; set; }
        public string ProviderSpecifiedReference { get; set; }
        public string Value { get; set; }
        public string LearnAimRef { get; set; }
        public string SupplementaryDataPanelDate { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
