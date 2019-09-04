using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.FundingData.Database.EF.Interfaces
{
    public interface IESFFundingDataContext : IDisposable
    {
        IQueryable<ESFFundingData> ESFFundingDatas { get; }

        IQueryable<LatestProviderSubmission> LatestProviderSubmissions { get; }

        IQueryable<ESFFundingDataSummarised> ESFFundingDatasSummarised { get; }
    }
}
