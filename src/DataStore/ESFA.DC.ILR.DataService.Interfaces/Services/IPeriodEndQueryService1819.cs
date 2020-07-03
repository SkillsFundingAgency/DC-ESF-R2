using System.Collections.Generic;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models.PeriodEnd;

namespace ESFA.DC.ILR.DataService.Interfaces.Services
{
    public interface IPeriodEndQueryService1819
    {
        Task<IEnumerable<PeriodEndMetrics>> GetPeriodEndMetrics(int periodId);
    }
}