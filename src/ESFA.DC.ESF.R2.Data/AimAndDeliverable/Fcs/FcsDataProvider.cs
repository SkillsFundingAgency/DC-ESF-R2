using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.Data.AimAndDeliverable.Fcs
{
    public class FcsDataProvider : IFcsDataProvider
    {
        private readonly Func<SqlConnection> _sqlConnectionFunc;

        private readonly string fcssql = @"SELECT [ExternalDeliverableCode], [DeliverableName] , [Round2DeliverableName]
                                            FROM [dbo].[ContractDeliverableCodeMapping]
                                              WHERE FundingStreamPeriodCode = 'ESF1420'";

        public FcsDataProvider(Func<SqlConnection> sqlConnectionFunc)
        {
            _sqlConnectionFunc = sqlConnectionFunc;
        }
        public async Task<ICollection<FCSDeliverableCodeMapping>> GetFcsDeliverableCodeMappingsAsync(CancellationToken cancellationToken)
        {
            using (var connection = _sqlConnectionFunc())
            {
                var result = await connection.QueryAsync<FCSDeliverableCodeMapping>(fcssql);

                return result.ToList();
            }
        }
    }
}
