//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
//using ESFA.DC.ESF.R2.Models.Ilr;
//using ESFA.DC.ESF.R2.Utils;
//using ESFA.DC.ILR1819.DataStore.EF.Interface;

//namespace ESFA.DC.ESF.R2.DataAccessLayer
//{
//    public class FM70Repository : IFM70Repository
//    {
//        private readonly Func<IILR1819_DataStoreEntities> _validContext;

//        public FM70Repository(
//            Func<IILR1819_DataStoreEntities> context)
//        {
//            _validContext = context;
//        }

//        public async Task<ILRFileDetailsModel> GetFileDetails(int ukPrn, CancellationToken cancellationToken)
//        {
//            ILRFileDetailsModel fileDetail;

//            if (cancellationToken.IsCancellationRequested)
//            {
//                return null;
//            }

//            using (var context = _validContext())
//            {
//                fileDetail = await context.FileDetails
//                    .Where(fd => fd.Ukprn == ukPrn)
//                    .OrderBy(fd => fd.SubmittedTime)
//                    .Select(fd => new ILRFileDetailsModel
//                    {
//                        FileName = fd.Filename,
//                        LastSubmission = fd.SubmittedTime
//                    })
//                    .FirstOrDefaultAsync(cancellationToken);
//            }

//            if (fileDetail != null && !string.IsNullOrEmpty(fileDetail.FileName))
//            {
//                fileDetail.Year = FileNameHelper.GetFundingYearFromILRFileName(fileDetail.FileName);
//            }

//            return fileDetail;
//        }

//        public async Task<IEnumerable<Fm70LearningDeliveryModel>> GetLearningDeliveries(int ukPrn, CancellationToken cancellationToken)
//        {
//            if (cancellationToken.IsCancellationRequested)
//            {
//                return null;
//            }

//            IEnumerable<Fm70LearningDeliveryModel> learningDeliveries;
//            using (var context = _validContext())
//            {
//                learningDeliveries = await context.EsfLearningDeliveries
//                    .Where(v => v.Ukprn == ukPrn)
//                    .Select(ld => new Fm70LearningDeliveryModel
//                    {
//                        UkPrn = ld.Ukprn,
//                        LearnRefNumber = ld.LearnRefNumber,
//                        AimSeqNumber = ld.AimSeqNumber,
//                        AimValue = ld.AimValue,
//                        AdjustedAreaCostFactor = ld.AdjustedAreaCostFactor,
//                        AdjustedPremiumFactor = ld.AdjustedPremiumFactor,
//                        ApplicWeightFundRate = ld.ApplicWeightFundRate,
//                        LdEsfEngagementStartDate = ld.LdesfengagementStartDate,
//                        LatestPossibleStartDate = ld.LatestPossibleStartDate,
//                        EligibleProgressionOutcomeType = ld.EligibleProgressionOutcomeType,
//                        EligibleProgressionOutcomeCode = ld.EligibleProgressionOutcomeCode,
//                        EligibleProgressionOutomeStartDate = ld.EligibleProgressionOutomeStartDate,
//                        Fm70LearningDeliveryDeliverables = ld.EsfLearningDeliveryDeliverables
//                            .Select(ldd => new Fm70LearningDeliveryDeliverableModel
//                        {
//                                UkPrn = ldd.Ukprn,
//                                LearnRefNumber = ldd.LearnRefNumber,
//                                AimSeqNumber = ldd.AimSeqNumber,
//                                DeliverableCode = ldd.DeliverableCode,
//                                DeliverableUnitCost = ldd.DeliverableUnitCost
//                        }),
//                        Fm70LearningDeliveryDeliverablePeriods = ld.EsfLearningDeliveryDeliverablePeriods
//                            .Select(lddp => new Fm70LearningDeliveryDeliverablePeriodModel
//                        {
//                                UkPrn = lddp.Ukprn,
//                                LearnRefNumber = lddp.LearnRefNumber,
//                                AimSeqNumber = lddp.AimSeqNumber,
//                                DeliverableCode = lddp.DeliverableCode,
//                                Period = lddp.Period,
//                                StartEarnings = lddp.StartEarnings,
//                                AchievementEarnings = lddp.AchievementEarnings,
//                                AdditionalProgCostEarnings = lddp.AdditionalProgCostEarnings,
//                                DeliverableVolume = lddp.DeliverableVolume,
//                                ProgressionEarnings = lddp.ProgressionEarnings,
//                                ReportingVolume = lddp.ReportingVolume
//                            })
//                    })
//                    .ToListAsync(cancellationToken);
//            }

//            return learningDeliveries;
//        }

//        public async Task<IList<FM70PeriodisedValuesModel>> GetPeriodisedValues(int ukPrn, CancellationToken cancellationToken)
//        {
//            IList<FM70PeriodisedValuesModel> values;

//            if (cancellationToken.IsCancellationRequested)
//            {
//                return null;
//            }

//            using (var context = _validContext())
//            {
//                values = await context.EsfLearningDeliveryDeliverablePeriodisedValues
//                    .Where(v => v.Ukprn == ukPrn)
//                    .Select(v => new FM70PeriodisedValuesModel
//                    {
//                        FundingYear = 2018,
//                        UKPRN = v.Ukprn,
//                        LearnRefNumber = v.LearnRefNumber,
//                        DeliverableCode = v.DeliverableCode,
//                        AimSeqNumber = v.AimSeqNumber,
//                        AttributeName = v.AttributeName,
//                        Period1 = v.Period1,
//                        Period2 = v.Period2,
//                        Period3 = v.Period3,
//                        Period4 = v.Period4,
//                        Period5 = v.Period5,
//                        Period6 = v.Period6,
//                        Period7 = v.Period7,
//                        Period8 = v.Period8,
//                        Period9 = v.Period9,
//                        Period10 = v.Period10,
//                        Period11 = v.Period11,
//                        Period12 = v.Period12
//                    })
//                    .ToListAsync(cancellationToken);
//            }

//            return values;
//        }

//        public async Task<IEnumerable<Fm70DpOutcome>> GetOutcomes(int ukPrn, CancellationToken cancellationToken)
//        {
//            IEnumerable<Fm70DpOutcome> outcomes;

//            if (cancellationToken.IsCancellationRequested)
//            {
//                return null;
//            }

//            using (var context = _validContext())
//            {
//                outcomes = await context.EsfDpoutcomes
//                    .Where(v => v.Ukprn == ukPrn)
//                    .Select(o => new Fm70DpOutcome
//                    {
//                        UkPrn = o.Ukprn,
//                        LearnRefNumber = o.LearnRefNumber,
//                        OutType = o.OutType,
//                        OutCode = o.OutCode,
//                        OutStartDate = o.OutStartDate,
//                        OutcomeDateForProgression = o.OutcomeDateForProgression
//                    })
//                    .ToListAsync(cancellationToken);
//            }

//            return outcomes;
//        }
//    }
//}
