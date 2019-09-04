using System.Linq;
using ESFA.DC.ESF.FundingData.Database.EF.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.FundingData.Database.EF
{
    public partial class ESFFundingDataContext : IESFFundingDataContext
    {
        IQueryable<ESFFundingDataSummarised> IESFFundingDataContext.ESFFundingDatasSummarised => vw_ESFFundingDataSummarised;

        IQueryable<ESFFundingData> IESFFundingDataContext.ESFFundingDatas => vw_ESFFundingDatas;

        IQueryable<LatestProviderSubmission> IESFFundingDataContext.LatestProviderSubmissions => vw_LatestProviderSubmissions;
    }
}
