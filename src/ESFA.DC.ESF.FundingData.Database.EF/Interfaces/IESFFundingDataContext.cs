using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.FundingData.Database.EF.Interfaces
{
    public interface IESFFundingDataContext : IDisposable
    {
        IQueryable<Query.ESFFundingData> ESFFundingDatas { get; }

        IQueryable<Query.LatestProviderSubmission> LatestProviderSubmissions { get; }

        IQueryable<ESFFundingDataSummarised> ESFFundingDatasSummarised { get; }
    }
}
