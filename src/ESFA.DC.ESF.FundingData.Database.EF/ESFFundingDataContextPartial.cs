using System.Linq;
using ESFA.DC.ESF.FundingData.Database.EF.Interfaces;

namespace ESFA.DC.ESF.FundingData.Database.EF
{
    public partial class ESFFundingDataContext : IESFFundingDataContext
    {
        IQueryable<ESFFundingData> IESFFundingDataContext.ESFFundingDatas => ESFFundingDatas;
    }
}
