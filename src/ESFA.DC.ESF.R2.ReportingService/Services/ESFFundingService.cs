using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.FundingData.Database.EF.Interfaces;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ESF.R2.ReportingService.Services
{
    public class ESFFundingService : IESFFundingService
    {
        private readonly Func<IESFFundingDataContext> _esfFundingDataContextFunc;

        public ESFFundingService(Func<IESFFundingDataContext> esfFundingDataContextFunc)
        {
            _esfFundingDataContextFunc = esfFundingDataContextFunc;
        }

        public async Task<string> GetLatestReturnCodeSubmittedForProvider(int ukprn, string collectionType, string collectionReturnCode, CancellationToken cancellationToken)
        {
            int.TryParse(collectionReturnCode.Substring(1), out var returnPeriod);

            using (var esfFundingDataContext = _esfFundingDataContextFunc.Invoke())
            {
                var returnPeriods = await esfFundingDataContext
                    .ESFFundingDatas.Where(fd =>
                        fd.UKPRN == ukprn &&
                        fd.CollectionType == collectionType)
                    .Select(fd => fd.CollectionReturnCode)
                    .Distinct()
                    .ToListAsync(cancellationToken);

                return returnPeriods
                    ?.Where(cr => int.Parse(cr.Substring(1)) <= returnPeriod)
                    .Max(fd => fd);
            }
        }

        public async Task<IEnumerable<FM70PeriodisedValues>> GetLatestFundingDataForProvider(int ukprn, int collectionYear, string collectionType, string collectionReturnCode, CancellationToken cancellationToken)
        {
            using (var esfFundingDataContext = _esfFundingDataContextFunc.Invoke())
            {
                return await esfFundingDataContext
                    .ESFFundingDatas.Where(fd =>
                        fd.UKPRN == ukprn &&
                        fd.CollectionType == collectionType &&
                        fd.CollectionReturnCode == collectionReturnCode)
                    .Select(fd => new FM70PeriodisedValues
                    {
                        UKPRN = fd.UKPRN,
                        AimSeqNumber = fd.AimSeqNumber,
                        AttributeName = fd.AttributeName,
                        ConRefNumber = fd.ConRefNumber,
                        DeliverableCode = fd.DeliverableCode,
                        LearnRefNumber = fd.LearnRefNumber,
                        FundingYear = collectionYear,
                        Period1 = fd.Period_1,
                        Period2 = fd.Period_2,
                        Period3 = fd.Period_3,
                        Period4 = fd.Period_4,
                        Period5 = fd.Period_5,
                        Period6 = fd.Period_6,
                        Period7 = fd.Period_7,
                        Period8 = fd.Period_8,
                        Period9 = fd.Period_9,
                        Period10 = fd.Period_10,
                        Period11 = fd.Period_11,
                        Period12 = fd.Period_12
                    })
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
