//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESFA.DC.ESF.R2.Database.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class SupplementaryDataUnitCost
    {
        public string ConRefNumber { get; set; }
        public string DeliverableCode { get; set; }
        public int CalendarYear { get; set; }
        public int CalendarMonth { get; set; }
        public string CostType { get; set; }
        public string StaffName { get; set; }
        public string ReferenceType { get; set; }
        public string Reference { get; set; }
        public Nullable<decimal> Value { get; set; }
    
        public virtual SupplementaryData SupplementaryData { get; set; }
    }
}
