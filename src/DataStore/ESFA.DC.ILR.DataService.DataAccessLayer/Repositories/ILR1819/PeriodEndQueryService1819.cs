using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1819EF.Valid;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models.PeriodEnd;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1819
{
    public class PeriodEndQueryService1819 : IPeriodEndQueryService1819
    {
        private readonly Func<ILR1819ValidLearnerContext> _contextFactory;

        public PeriodEndQueryService1819(Func<ILR1819ValidLearnerContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<IEnumerable<PeriodEndMetrics>> GetPeriodEndMetrics(int periodId)
        {
            List<PeriodEndMetrics> metrics;

            using (var context = _contextFactory())
            {
                metrics = await context.PeriodEndMetrics
                    .FromSql("GetPeriodEndMetrics @periodId", new SqlParameter("@periodId", periodId))
                    .Select(entity => new PeriodEndMetrics
                    {
                        TransactionType = entity.TransactionType,
                        EarningsYTD = entity.EarningsYTD,
                        EarningsACT1 = entity.EarningsACT1,
                        EarningsACT2 = entity.EarningsACT2
                    }).ToListAsync();
            }

            return metrics;
        }
    }
}