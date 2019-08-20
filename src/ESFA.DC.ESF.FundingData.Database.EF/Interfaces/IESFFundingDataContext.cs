using System;
using System.Linq;

namespace ESFA.DC.ESF.FundingData.Database.EF.Interfaces
{
    public interface IESFFundingDataContext : IDisposable
    {
        IQueryable<ESFFundingData> ESFFundingDatas { get; }

        IQueryable<LatestProviderSubmission> LatestProviderSubmissions { get; }
    }
}
