using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Service.Mappers
{
    public sealed class ESFMapper : ClassMap<SupplementaryDataModel>, IClassMapper
    {
        public ESFMapper()
        {
            int i = 0;
            Map(m => m.ConRefNumber).Index(i++);
            Map(m => m.DeliverableCode).Index(i++);
            Map(m => m.CalendarYear).Index(i++);
            Map(m => m.CalendarMonth).Index(i++);
            Map(m => m.CostType).Index(i++);
            Map(m => m.ReferenceType).Index(i++);
            Map(m => m.Reference).Index(i++);
            Map(m => m.ULN).Index(i++);
            Map(m => m.ProviderSpecifiedReference).Index(i++);
            Map(m => m.Value).Index(i++);
            Map(m => m.LearnAimRef).Index(i++);
            Map(m => m.SupplementaryDataPanelDate).Index(i);
        }
    }
}
