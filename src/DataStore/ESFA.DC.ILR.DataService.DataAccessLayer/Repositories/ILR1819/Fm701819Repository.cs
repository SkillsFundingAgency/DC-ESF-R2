using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1819EF.Rulebase;
using ESFA.DC.ILR.DataService.ILR1819EF.Valid;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using ESFA.DC.ILR.DataService.Utils;
using Microsoft.EntityFrameworkCore;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1819
{
    public class Fm701819Repository : IFm701819Repository
    {
        private readonly Func<ILR1819RuleBaseContext> _rulebaseContext;
        private readonly Func<ILR1819ValidLearnerContext> _validContext;

        public Fm701819Repository(Func<ILR1819RuleBaseContext> rulebaseContext, Func<ILR1819ValidLearnerContext> validContext)
        {
            _rulebaseContext = rulebaseContext;
            _validContext = validContext;
        }

        public async Task<ILRFileDetails> GetFileDetails(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            ILRFileDetails fileDetail;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _rulebaseContext())
            {
                fileDetail = await context.FileDetails
                    .Where(fd => fd.Ukprn == ukPrn)
                    .OrderBy(fd => fd.SubmittedTime)
                    .Select(fd => new ILRFileDetails
                    {
                        FileName = fd.Filename,
                        LastSubmission = fd.SubmittedTime
                    })
                    .FirstOrDefaultAsync(cancellationToken);
            }

            if (fileDetail != null && !string.IsNullOrEmpty(fileDetail.FileName))
            {
                fileDetail.Year = FileNameHelper.GetFundingYearFromILRFileName(fileDetail.FileName);
            }

            return fileDetail;
        }

        public async Task<IEnumerable<Fm70LearningDelivery>> GetLearningDeliveries(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<Fm70LearningDelivery> learningDeliveries;
            using (var context = _rulebaseContext())
            {
                learningDeliveries = await context.EsfLearningDeliveries
                    .Where(v => v.Ukprn == ukPrn)
                    .Select(ld => new Fm70LearningDelivery
                    {
                        UkPrn = ld.Ukprn,
                        LearnRefNumber = ld.LearnRefNumber,
                        AimSeqNumber = ld.AimSeqNumber,
                        AimValue = ld.AimValue,
                        AdjustedAreaCostFactor = ld.AdjustedAreaCostFactor,
                        AdjustedPremiumFactor = ld.AdjustedPremiumFactor,
                        ApplicWeightFundRate = ld.ApplicWeightFundRate,
                        LdEsfEngagementStartDate = ld.LdesfengagementStartDate,
                        LatestPossibleStartDate = ld.LatestPossibleStartDate,
                        EligibleProgressionOutcomeType = ld.EligibleProgressionOutcomeType,
                        EligibleProgressionOutcomeCode = ld.EligibleProgressionOutcomeCode,
                        EligibleProgressionOutomeStartDate = ld.EligibleProgressionOutomeStartDate,
                        Fm70LearningDeliveryDeliverables = ld.EsfLearningDeliveryDeliverables
                            .Select(ldd => new Fm70LearningDeliveryDeliverable
                        {
                                UkPrn = ldd.Ukprn,
                                LearnRefNumber = ldd.LearnRefNumber,
                                AimSeqNumber = ldd.AimSeqNumber,
                                DeliverableCode = ldd.DeliverableCode,
                                DeliverableUnitCost = ldd.DeliverableUnitCost
                        }),
                        Fm70LearningDeliveryDeliverablePeriods = ld.EsfLearningDeliveryDeliverablePeriods
                            .Select(lddp => new Fm70LearningDeliveryDeliverablePeriod
                        {
                                UkPrn = lddp.Ukprn,
                                LearnRefNumber = lddp.LearnRefNumber,
                                AimSeqNumber = lddp.AimSeqNumber,
                                DeliverableCode = lddp.DeliverableCode,
                                Period = lddp.Period,
                                StartEarnings = lddp.StartEarnings,
                                AchievementEarnings = lddp.AchievementEarnings,
                                AdditionalProgCostEarnings = lddp.AdditionalProgCostEarnings,
                                DeliverableVolume = lddp.DeliverableVolume,
                                ProgressionEarnings = lddp.ProgressionEarnings,
                                ReportingVolume = lddp.ReportingVolume
                            })
                    })
                    .ToListAsync(cancellationToken);
            }

            return learningDeliveries;
        }

        public async Task<IEnumerable<FM70PeriodisedValues>> GetPeriodisedValues(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            IList<FM70PeriodisedValues> values;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _rulebaseContext())
            {
                values = await context.EsfLearningDeliveryDeliverablePeriodisedValues
                    .Where(v => v.Ukprn == ukPrn)
                    .Select(v => new FM70PeriodisedValues
                    {
                        FundingYear = 2018,
                        UKPRN = v.Ukprn,
                        LearnRefNumber = v.LearnRefNumber,
                        DeliverableCode = v.DeliverableCode,
                        AimSeqNumber = v.AimSeqNumber,
                        AttributeName = v.AttributeName,
                        ConRefNumber = GetConRefNumber(ukPrn, v.LearnRefNumber, v.AimSeqNumber),
                        Period1 = v.Period1,
                        Period2 = v.Period2,
                        Period3 = v.Period3,
                        Period4 = v.Period4,
                        Period5 = v.Period5,
                        Period6 = v.Period6,
                        Period7 = v.Period7,
                        Period8 = v.Period8,
                        Period9 = v.Period9,
                        Period10 = v.Period10,
                        Period11 = v.Period11,
                        Period12 = v.Period12
                    })
                    .ToListAsync(cancellationToken);
            }

            return values;
        }

        public async Task<IEnumerable<Fm70DpOutcome>> GetOutcomes(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            IEnumerable<Fm70DpOutcome> outcomes;

            cancellationToken.ThrowIfCancellationRequested();

            using (var context = _rulebaseContext())
            {
                outcomes = await context.EsfDpoutcomes
                    .Where(v => v.Ukprn == ukPrn)
                    .Select(o => new Fm70DpOutcome
                    {
                        UkPrn = o.Ukprn,
                        LearnRefNumber = o.LearnRefNumber,
                        OutType = o.OutType,
                        OutCode = o.OutCode,
                        OutStartDate = o.OutStartDate,
                        OutcomeDateForProgression = o.OutcomeDateForProgression
                    })
                    .ToListAsync(cancellationToken);
            }

            return outcomes;
        }

        private string GetConRefNumber(int ukPrn, string learnRefNumber, int aimSeqNumber)
        {
            using (var context = _validContext())
            {
                return context.LearningDeliveries
                    .SingleOrDefault(ld => ld.Ukprn == ukPrn && ld.LearnRefNumber == learnRefNumber && ld.AimSeqNumber == aimSeqNumber)?.ConRefNumber ?? string.Empty;
            }
        }
    }
}
