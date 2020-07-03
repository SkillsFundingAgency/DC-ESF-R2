using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models.PeriodEnd;

namespace ESFA.DC.ILR.DataService.Services.PeriodEnd
{
    public class PeriodEndMetricsService1920 : IPeriodEndMetricsService1920
    {
        private readonly IPeriodEndQueryService1920 _queryService;

        public PeriodEndMetricsService1920(IPeriodEndQueryService1920 queryService)
        {
            _queryService = queryService;
        }

        public async Task<IEnumerable<PeriodEndMetrics>> GetPeriodEndMetrics(int periodId)
        {
            return await _queryService.GetPeriodEndMetrics(periodId);
        }
    }
}