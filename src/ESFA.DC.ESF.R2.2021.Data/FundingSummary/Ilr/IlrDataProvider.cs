using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using Dapper;

namespace ESFA.DC.ESF.R2._2021.Data.FundingSummary.Ilr
{
    public class IlrDataProvider : IIlrDataProvider
    {
        private readonly string IlrfileDetailsSql = @"SELECT TOP (1) f.[Filename], f.[SubmittedTime], c.[FilePreparationDate]
                                                      FROM [dbo].[FileDetails] f
                                                      INNER JOIN [Valid].[CollectionDetails] c 
                                                      ON c.[UKPRN] = f.[UKPRN]
                                                      WHERE f.[UKPRN] = @ukprn
                                                      ORDER BY f.[SubmittedTime] DESC;";

        private readonly string EsfReturnPeriodSql = @"SELECT DISTINCT([CollectionReturnCode])
                                                       FROM [dbo].[LatestProviderSubmission]
                                                       WHERE [UKPRN] = @ukprn
                                                       AND [CollectionType] = '@collectionType'";

        private readonly string EsfFundingDataSql = @"SELECT 
                                                        [UKPRN],
                                                        [AimSeqNumber],
                                                        [AttributeName],
                                                        [ConRefNumber],
                                                        [DeliverableCode],
                                                        [LearnRefNumber],
                                                        '@collectionYear' AS FundingYear,
                                                        [Period_1] AS Period1, 
                                                        [Period_2] AS Period2,
                                                        [Period_3] AS Period3,
                                                        [Period_4] AS Period4,
                                                        [Period_5] AS Period5, 
                                                        [Period_6] AS Period6,
                                                        [Period_7] AS Period7, 
                                                        [Period_8] AS Period8,
                                                        [Period_9] AS Period9,
                                                        [Period_10] AS Period10,
                                                        [Period_11] AS Period11,
                                                        [Period_12] AS Period12
                                                      FROM [dbo].[ESFFundingData]
                                                      WHERE [UKPRN] = @ukprn
                                                      AND [CollectionType] = '@collectionType'
                                                      AND [CollectionReturnCode] = '@returnCode'";

        private readonly IDictionary<int, Func<SqlConnection>> _ilrSqlConnectionFunc;
        private readonly Func<SqlConnection> _esfSqlConnectionFunc;
        private readonly IReturnPeriodLookup _returnPeriodLookup;

        public IlrDataProvider(
            IDictionary<int, Func<SqlConnection>> ilrSqlConnectionFunc,
            Func<SqlConnection> esfSqlConnectionFunc,
            IReturnPeriodLookup returnPeriodLookup)
        {
            _ilrSqlConnectionFunc = ilrSqlConnectionFunc;
            _esfSqlConnectionFunc = esfSqlConnectionFunc;
            _returnPeriodLookup = returnPeriodLookup;
        }

        public async Task<ICollection<ILRFileDetails>> GetIlrFileDetailsAsync(int ukprn, CancellationToken cancellationToken)
        {
            var fileDetails = new List<ILRFileDetails>();

            var fileDetail2021 = await RetrieveFileDetails(ukprn, AcademicYearConstants.Year2020);
            var fileDetail1920 = await RetrieveFileDetails(ukprn, AcademicYearConstants.Year2019);
            var fileDetail1819 = await RetrieveFileDetails(ukprn, AcademicYearConstants.Year2018);

            if (fileDetail2021 != null) { fileDetails.Add(fileDetail2021); }
            if (fileDetail1920 != null) { fileDetails.Add(fileDetail1920); }
            if (fileDetail1819 != null) { fileDetails.Add(fileDetail1819); }

            return fileDetails;
        }
        public async Task<ICollection<FM70PeriodisedValues>> GetIlrPeriodisedValuesAsync(int ukprn, string returnPeriod, CancellationToken cancellationToken)
        {
            var periodisedValues = new List<FM70PeriodisedValues>();

            var previousYearReturnPeriod = _returnPeriodLookup.GetReturnPeriodForPreviousCollectionYear(returnPeriod);

            periodisedValues.AddRange(await GetAcademicYearIlrData(ukprn, AcademicYearConstants.Year2018, IlrConstants.ILR1819Collection, previousYearReturnPeriod, cancellationToken));
            periodisedValues.AddRange(await GetAcademicYearIlrData(ukprn, AcademicYearConstants.Year2019, IlrConstants.ILR1920Collection, previousYearReturnPeriod, cancellationToken));
            periodisedValues.AddRange(await GetAcademicYearIlrData(ukprn, AcademicYearConstants.Year2020, IlrConstants.ILR2021Collection, returnPeriod, cancellationToken));

            return periodisedValues;
        }

        private async Task<IEnumerable<FM70PeriodisedValues>> GetAcademicYearIlrData(int ukprn, int collectionYear, string collectionType, string collectionReturnCode, CancellationToken cancellationToken)
        {
            int.TryParse(collectionReturnCode.Substring(1), out var returnPeriod);

            var esfReturnPeriodCodes = await RetrieveLatestEsfReturnCode(ukprn, collectionType);
            var returnCode = esfReturnPeriodCodes?.Where(cr => int.Parse(cr.Substring(1)) <= returnPeriod).Max(fd => fd);

            if (returnCode != null)
            {
                var providerData = await RetrieveLatestEsfFundingData(ukprn, collectionYear, collectionType, returnCode);

                return providerData;
            }

            return Enumerable.Empty<FM70PeriodisedValues>();
        }

        private async Task<ILRFileDetails> RetrieveFileDetails(int ukprn, int year)
        {
            using (var connection = _ilrSqlConnectionFunc[year]())
            {
                var result = await connection.QuerySingleAsync<ILRFileDetails>(IlrfileDetailsSql, new { ukprn });

                return result;
            }
        }

        private async Task<IEnumerable<string>> RetrieveLatestEsfReturnCode(int ukprn, string collectionType)
        {
            using (var connection = _esfSqlConnectionFunc())
            {
                var result = await connection.QueryAsync<string>(EsfReturnPeriodSql, new { ukprn, collectionType });

                return result.ToList();
            }
        }

        private async Task<ICollection<FM70PeriodisedValues>> RetrieveLatestEsfFundingData(int ukprn, int collectionYear, string collectionType, string returnCode)
        {
            using (var connection = _esfSqlConnectionFunc())
            {
                var result = await connection.QueryAsync<FM70PeriodisedValues>(EsfFundingDataSql, new { ukprn, collectionYear, collectionType, returnCode });

                return result.ToList();
            }
        }
    }
}
