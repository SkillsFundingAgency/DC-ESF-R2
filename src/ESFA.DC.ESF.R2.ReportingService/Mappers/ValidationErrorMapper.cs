using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Mappers
{
    public sealed class ValidationErrorMapper : ClassMap<ValidationErrorModel>, IClassMapper
    {
        public ValidationErrorMapper()
        {
            int i = 0;
            Map(m => m.IsWarning).ConvertUsing(c => c.IsWarning ? "W" : "E").Index(i++).Name("Error/Warning");
            Map(m => m.RuleName).Index(i++).Name("RuleName");
            Map(m => m.ErrorMessage).Index(i++).Name("ErrorMessage");
            Map(m => m.ConRefNumber).Index(i++).Name("ConRefNumber");
            Map(m => m.DeliverableCode).Index(i++).Name("DeliverableCode");
            Map(m => m.CalendarYear).Index(i++).Name("CalendarYear");
            Map(m => m.CalendarMonth).Index(i++).Name("CalendarMonth");
            Map(m => m.CostType).Index(i++).Name("CostType");
            Map(m => m.StaffName).Index(i++).Name("StaffName");
            Map(m => m.ReferenceType).Index(i++).Name("ReferenceType");
            Map(m => m.Reference).Index(i++).Name("Reference");
            Map(m => m.ULN).Index(i++).Name("ULN");
            Map(m => m.ProviderSpecifiedReference).Index(i++).Name("ProviderSpecifiedReference");
            Map(m => m.Value).Index(i++).Name("Value");
            Map(m => m.LearnAimRef).Index(i++).Name("LearnAimRef");
            Map(m => m.SupplementaryDataPanelDate).Index(i++).Name("SupplementaryDataPanelDate");
            Map(m => m.OfficialSensitive).Index(i).Name("OFFICIAL – SENSITIVE");
        }
    }
}
