﻿namespace ESFA.DC.ESF.R2.Models
{
    public class SupplementaryDataLooseModel
    {
        public string ConRefNumber { get; set; }

        public string DeliverableCode { get; set; }

        public string CalendarYear { get; set; }

        public string CalendarMonth { get; set; }

        public string CostType { get; set; }

        public string StaffName { get; set; }

        public string ReferenceType { get; set; }

        public string Reference { get; set; }

        public string ULN { get; set; }

        public string ProviderSpecifiedReference { get; set; }

        public string Value { get; set; }

        public string HourlyRate { get; set; }

        public string TotalHoursWorked { get; set; }

        public string ProjectHours { get; set; }

        public string OrgHours { get; set; }

        public string OfficialSensitive { get; }
    }
}