using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.FundingData.Database.EF.Query;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Services
{
    public interface IESFFundingService
    {
        Task<IEnumerable<FM70PeriodisedValues>> GetLatestFundingDataForProvider(int ukprn, int collectionYear,
            string collectionType, string collectionReturnCode, CancellationToken cancellationToken);
    }
}
