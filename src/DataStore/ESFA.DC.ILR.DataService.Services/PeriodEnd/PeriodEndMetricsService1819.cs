using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models.PeriodEnd;

namespace ESFA.DC.ILR.DataService.Services.PeriodEnd
{
    public class PeriodEndMetricsService1819 : IPeriodEndMetricsService1819
    {
        private readonly IPeriodEndQueryService1819 _queryService;

        public PeriodEndMetricsService1819(IPeriodEndQueryService1819 queryService)
        {
            _queryService = queryService;
        }

        public async Task<IEnumerable<PeriodEndMetrics>> GetPeriodEndMetrics(int periodId)
        {
            return await _queryService.GetPeriodEndMetrics(periodId);
        }
    }
}