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
        private readonly IESFFundingDataContext _esfFundingDataContext;

        public ESFFundingService(IESFFundingDataContext esfFundingDataContext)
        {
            _esfFundingDataContext = esfFundingDataContext;
        }

        public async Task<IEnumerable<FM70PeriodisedValues>> GetLatestFundingDataForProvider(int ukprn, int collectionYear, string collectionType, string collectionReturnCode, CancellationToken cancellationToken)
        {
            var returnCode = await _esfFundingDataContext
                .ESFFundingDatas.Where(fd =>
                    fd.UKPRN == ukprn &&
                    fd.CollectionType == collectionType)
                .Select(fd => fd.CollectionReturnCode)
                .Distinct()
                .Where(cr => string.Compare(cr, collectionReturnCode, StringComparison.OrdinalIgnoreCase) <= 0)
                .MaxAsync(fd => fd, cancellationToken);

            return await _esfFundingDataContext
                .ESFFundingDatas.Where(fd =>
                    fd.CollectionType == collectionType &&
                    fd.UKPRN == ukprn & fd.CollectionReturnCode == returnCode)
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
