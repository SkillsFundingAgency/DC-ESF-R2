﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;
using ESFA.DC.Serialization.Interfaces;

namespace ESFA.DC.ESF.R2.Data.AimAndDeliverable.Lars
{
    public class LarsDataProvider : ILarsDataProvider
    {
        private readonly Func<SqlConnection> _sqlConnectionFunc;
        private readonly IJsonSerializationService _jsonSerializationService;

        private readonly string sql = @"SELECT DISTINCT
                                         L.[LearnAimRef]
                                        ,[LearnAimRefTitle]
                                        ,[NotionalNVQLevelV2]
                                        ,[SectorSubjectAreaTier2]
                                    FROM OPENJSON(@learnAimRefsSerialized) 
                                    WITH (LearnAimRef nvarchar(8) '$') J 
                                    INNER JOIN [Core].[LARS_LearningDelivery] L
                                    ON L.[LearnAimRef] = J.[LearnAimRef]";

        public LarsDataProvider(Func<SqlConnection> sqlConnectionFunc, IJsonSerializationService jsonSerializationService)
        {
            _sqlConnectionFunc = sqlConnectionFunc;
            _jsonSerializationService = jsonSerializationService;
        }

        public async Task<ICollection<LARSLearningDelivery>> GetLarsLearningDeliveriesAsync(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken)
        {
            using (var connection = _sqlConnectionFunc())
            {
                var learnAimRefsSerialized = _jsonSerializationService.Serialize(learnAimRefs);

                var commandDefinition = new CommandDefinition(sql, new { learnAimRefsSerialized }, cancellationToken: cancellationToken);

                var result = await connection.QueryAsync<LARSLearningDelivery>(commandDefinition);

                return result.ToList();
            }
        }
    }
}
