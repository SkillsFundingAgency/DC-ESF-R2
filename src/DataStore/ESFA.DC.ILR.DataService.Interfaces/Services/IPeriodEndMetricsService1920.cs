using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models.PeriodEnd;

namespace ESFA.DC.ILR.DataService.Interfaces.Services
{
    public interface IPeriodEndMetricsService1920
    {
        Task<IEnumerable<PeriodEndMetrics>> GetPeriodEndMetrics(int periodId);
    }
}