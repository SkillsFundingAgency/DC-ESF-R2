﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1617EF;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1617
{
    public class Fm701617Repository : IFm701617Repository
    {
        // private readonly ILogger _logger;
        private readonly Func<ILR1617Context> _1617Context;

        public Fm701617Repository(Func<ILR1617Context> ilr1617Context)
        {
            _1617Context = ilr1617Context;
        }

        public async Task<IEnumerable<FM70PeriodisedValues>> Get1617PeriodisedValues(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            IList<FM70PeriodisedValues> values;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _1617Context())
            {
                values = await context.EsfLearningDeliveryDeliverablePeriodisedValues
                    .Where(v => v.Ukprn == ukPrn)
                    .Select(pv => new FM70PeriodisedValues
                    {
                        FundingYear = 2016,
                        AimSeqNumber = pv.AimSeqNumber,
                        AttributeName = pv.AttributeName,
                        DeliverableCode = pv.DeliverableCode,
                        LearnRefNumber = pv.LearnRefNumber,
                        ConRefNumber = string.Empty,
                        Period1 = pv.Period1,
                        Period2 = pv.Period2,
                        Period3 = pv.Period3,
                        Period4 = pv.Period4,
                        Period5 = pv.Period5,
                        Period6 = pv.Period6,
                        Period7 = pv.Period7,
                        Period8 = pv.Period8,
                        Period9 = pv.Period9,
                        Period10 = pv.Period10,
                        Period11 = pv.Period11,
                        Period12 = pv.Period12,
                    })
                    .ToListAsync(cancellationToken);
            }

            return values;
        }
    }
}