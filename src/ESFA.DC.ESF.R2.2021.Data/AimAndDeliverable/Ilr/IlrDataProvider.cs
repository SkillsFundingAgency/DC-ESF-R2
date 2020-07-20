﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable.DataProvider._2021;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2._2021.Data.AimAndDeliverable.Ilr
{
    public class IlrDataProvider : IIlrDataProvider
    {
        private readonly Func<SqlConnection> _sqlConnectionFunc;

        private readonly string learningDeliverySql = @";WITH CTE_ProviderSpecLearnerMonitoring AS(SELECT UKPRN, LearnRefNumber, A as ProviderSpecLearnerMonitoring_A, B as ProviderSpecLearnerMonitoring_B
                                                        FROM   (SELECT[UKPRN]
                                                          ,[LearnRefNumber]
                                                          ,[ProvSpecLearnMonOccur]
                                                          ,[ProvSpecLearnMon]
                                                      FROM [Valid].[ProviderSpecLearnerMonitoring] WHERE UKPRN = @ukprn) AS t
                                                           PIVOT(max(ProvSpecLearnMon)
                                                                 FOR [ProvSpecLearnMonOccur] IN ([A], [B])) AS p)

                                                    , CTE_LearningDeliveryFAM AS(
                                                      SELECT* FROM (
                                                         SELECT[UKPRN], [LearnRefNumber], [AimSeqNumber],[LearnDelFAMType],[LearnDelFAMCode], CAST((ROW_NUMBER() OVER(PARTITION BY [LearnRefNumber], [AimSeqNumber] ORDER BY [LearnDelFAMCode] ))AS VARCHAR) AS RowNumber

                                                        FROM[Valid].[LearningDeliveryFAM]
                                                            WHERE UKPRN = @ukprn and LearnDelFAMType = 'RES') as S
                                                             WHERE RowNumber = 1
	                                                     )

                                                    ,CTE_ProviderSpecDeliveryMonitoring AS(SELECT UKPRN, LearnRefNumber, AimSeqNumber, A as ProviderSpecLearnerMonitoring_A, B as ProviderSpecLearnerMonitoring_B,
                                                                C as ProviderSpecLearnerMonitoring_C, D as ProviderSpecLearnerMonitoring_D
                                                    FROM   (SELECT[UKPRN]
                                                          ,[LearnRefNumber]
                                                          , AimSeqNumber
                                                          ,[ProvSpecDelMonOccur]
                                                          ,[ProvSpecDelMon]
                                                      FROM [Valid].[ProviderSpecDeliveryMonitoring] WHERE UKPRN = @ukprn) AS t
                                                           PIVOT(max(ProvSpecDelMon)
                                                                 FOR [ProvSpecDelMonOccur] IN ([A], [B], [C], [D])) AS p)


                                                    SELECT
                                                        L.ULN,
                                                        L.LearnRefNumber,
                                                        L.GivenNames,
                                                        L.FamilyName,
                                                        L.CampId,
                                                        L.PMUKPRN,
                                                        PSLM.ProviderSpecLearnerMonitoring_A,
                                                        PSLM.ProviderSpecLearnerMonitoring_B,
                                                        LD.ConRefNumber,
                                                        LD.LearnAimRef,
                                                        LD.AimSeqNumber,
                                                        LD.FundModel,
                                                        LD.LearnStartDate,
                                                        LD.LearnPlanEndDate,
                                                        LD.LearnActEndDate,
                                                        LD.CompStatus,
                                                        LD.DelLocPostCode,
                                                        LD.Outcome,
                                                        LD.AddHours,
                                                        LD.PartnerUKPRN,
                                                        LD.SWSupAimId,
                                                        LDFAM.LearnDelFAMCode AS LearningDeliveryFAM_RES,
                                                        PSDM.ProviderSpecLearnerMonitoring_A,
                                                        PSDM.ProviderSpecLearnerMonitoring_B,
                                                        PSDM.ProviderSpecLearnerMonitoring_C,
                                                        PSDM.ProviderSpecLearnerMonitoring_D,
                                                        ESFLD.ApplicWeightFundRate,
                                                        ESFLD.AimValue,
                                                        ESFLD.AdjustedAreaCostFactor,
                                                        ESFLD.AdjustedPremiumFactor,
                                                        ESFLD.LDESFEngagementStartDate,
                                                        ESFLD.LatestPossibleStartDate,
                                                        ESFLD.EligibleProgressionOutomeStartDate,
                                                        ESFLD.EligibleProgressionOutcomeType,
                                                        ESFLD.EligibleProgressionOutcomeCode
                                                      FROM [Valid].[LearningDelivery] LD (NOLOCK)
                                                      INNER JOIN[Valid].[Learner] L (NOLOCK)
                                                          ON LD.UKPRN = L.UKPRN

                                                          AND LD.LearnRefNumber = L.LearnRefNumber
                                                      LEFT JOIN CTE_ProviderSpecLearnerMonitoring PSLM

                                                            ON L.UKPRN = PSLM.UKPRN

                                                            AND L.LearnRefNumber = PSLM.LearnRefNumber
                                                     LEFT JOIN CTE_LearningDeliveryFAM LDFAM

                                                        ON LD.UKPRN = LDFAM.UKPRN

                                                        AND LD.LearnRefNumber = LDFAM.LearnRefNumber

                                                        AND LD.AimSeqNumber = LDFAM.AimSeqNumber
                                                    LEFT JOIN CTE_ProviderSpecDeliveryMonitoring PSDM

                                                            ON LD.UKPRN = PSLM.UKPRN

                                                            AND LD.LearnRefNumber = PSDM.LearnRefNumber

                                                            AND LD.AimSeqNumber = PSDM.AimSeqNumber
                                                    LEFT JOIN Rulebase.ESF_LearningDelivery ESFLD

                                                        ON LD.UKPRN = ESFLD.UKPRN

                                                        AND LD.LearnRefNumber = ESFLD.LearnRefNumber

                                                        AND LD.AimSeqNumber = ESFLD.AimSeqNumber
                                                      WHERE
                                                        L.UKPRN = @ukprn
                                                        AND LD.fundmodel = 70";


        private readonly string dpoutcomeSql = @"SELECT  [LearnRefNumber], [OutType], [OutCode], [OutStartDate], [OutEndDate], [OutCollDate] FROM [Valid].[DPOutcome] where UKPRN = @ukprn";

        private readonly string learningDeliveryDeliverablePeriodSql = @"SELECT LDD.LearnRefNumber, LDD.AimSeqNumber, LDD.DeliverableCode, LDD.DeliverableUnitCost,
                                                                            LDDP.Period, LDDP.DeliverableVolume, LDDP.ReportingVolume, LDDP.StartEarnings, LDDP.AchievementEarnings, LDDP.AdditionalProgCostEarnings, LDDP.ProgressionEarnings,
                                                                            LDDP.StartEarnings + LDDP.AchievementEarnings + LDDP.AdditionalProgCostEarnings + LDDP.ProgressionEarnings AS TotalEarnings 
                                                                            FROM 
                                                                            [Rulebase].[ESF_LearningDeliveryDeliverable] LDD
                                                                            INNER JOIN [Rulebase].[ESF_LearningDeliveryDeliverable_Period] LDDP
	                                                                            ON LDD.UKPRN = LDDP.UKPRN
	                                                                            AND LDD.LearnRefNumber = LDDP.LearnRefNumber
	                                                                            AND LDD.AimSeqNumber = LDDP.AimSeqNumber
	                                                                            AND LDD.DeliverableCode = LDDP.DeliverableCode
                                                                            WHERE UKPRN = @ukprn";

        private readonly string esfdpoutcomeSql = "SELECT [LearnRefNumber], [OutCode], [OutType], [OutStartDate], [OutcomeDateForProgression] FROM [Rulebase].[ESF_DPOutcome] WHERE UKPRN = @ukprn";

        public IlrDataProvider(Func<SqlConnection> sqlConnectionFunc)
        {
            _sqlConnectionFunc = sqlConnectionFunc;
        }

        public async Task<ICollection<LearningDelivery>> GetLearningDeliveriesAsync(int ukprn, CancellationToken cancellationToken)
        {
            using (var connection = _sqlConnectionFunc())
            {
                var result = await connection.QueryAsync<LearningDelivery>(learningDeliverySql, new { ukprn });

                return result.ToList();
            }
        }


        public async Task<ICollection<DPOutcome>> GetDpOutcomesAsync(int ukprn, CancellationToken cancellationToken)
        {
            using (var connection = _sqlConnectionFunc())
            {
                var result = await connection.QueryAsync<DPOutcome>(dpoutcomeSql, new { ukprn });

                return result.ToList();
            }
        }

        public async Task<ICollection<ESFLearningDeliveryDeliverablePeriod>> GetDeliverablePeriodsAsync(int ukprn, CancellationToken cancellationToken)
        {
            using (var connection = _sqlConnectionFunc())
            {
                var result = await connection.QueryAsync<ESFLearningDeliveryDeliverablePeriod>(learningDeliveryDeliverablePeriodSql, new { ukprn });

                return result.ToList();
            }
        }

        public async Task<ICollection<ESFDPOutcome>> GetEsfDpOutcomesAsync(int ukprn, CancellationToken cancellationToken)
        {
            using (var connection = _sqlConnectionFunc())
            {
                var result = await connection.QueryAsync<ESFDPOutcome>(esfdpoutcomeSql, new { ukprn });

                return result.ToList();
            }
        }

    }
}
