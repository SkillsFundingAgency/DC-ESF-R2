using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ILR1819.DataStore.EF;
using ESFA.DC.ILR1819.DataStore.EF.Interfaces;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class FM70Repository : IFM70Repository
    {
        private readonly Func<IILR1819_DataStoreEntities> _validContext;

        public FM70Repository(
            Func<IILR1819_DataStoreEntities> context)
        {
            _validContext = context;
        }

        public async Task<ILRFileDetailsModel> GetFileDetails(int ukPrn, CancellationToken cancellationToken)
        {
            ILRFileDetailsModel fileDetail;

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            using (var context = _validContext())
            {
                fileDetail = await context.FileDetails
                    .Where(fd => fd.UKPRN == ukPrn)
                    .OrderBy(fd => fd.SubmittedTime)
                    .Select(fd => new ILRFileDetailsModel
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

        public async Task<IList<ESF_LearningDelivery>> GetLearningDeliveries(int ukPrn, CancellationToken cancellationToken)
        {
            IList<ESF_LearningDelivery> values;

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            using (var context = _validContext())
            {
                values = await context.ESF_LearningDelivery
                    .Where(v => v.UKPRN == ukPrn)
                    .ToListAsync(cancellationToken);
            }

            return values;
        }

        public async Task<IList<ESF_LearningDeliveryDeliverable>> GetLearningDeliveryDeliverables(int ukPrn, CancellationToken cancellationToken)
        {
            IList<ESF_LearningDeliveryDeliverable> values;

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            using (var context = _validContext())
            {
                values = await context.ESF_LearningDeliveryDeliverable
                    .Where(v => v.UKPRN == ukPrn)
                    .ToListAsync(cancellationToken);
            }

            return values;
        }

        public async Task<IList<ESF_LearningDeliveryDeliverable_Period>> GetLearningDeliveryDeliverablePeriods(int ukPrn, CancellationToken cancellationToken)
        {
            IList<ESF_LearningDeliveryDeliverable_Period> values;

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            using (var context = _validContext())
            {
                values = await context.ESF_LearningDeliveryDeliverable_Period
                    .Where(v => v.UKPRN == ukPrn)
                    .ToListAsync(cancellationToken);
            }

            return values;
        }

        public async Task<IList<FM70PeriodisedValuesModel>> GetPeriodisedValues(int ukPrn, CancellationToken cancellationToken)
        {
            IList<FM70PeriodisedValuesModel> values;

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            using (var context = _validContext())
            {
                values = await context.ESF_LearningDeliveryDeliverable_PeriodisedValues
                    .Where(v => v.UKPRN == ukPrn)
                    .Select(v => new FM70PeriodisedValuesModel
                    {
                        FundingYear = 2018,
                        UKPRN = v.UKPRN,
                        LearnRefNumber = v.LearnRefNumber,
                        DeliverableCode = v.DeliverableCode,
                        AimSeqNumber = v.AimSeqNumber,
                        AttributeName = v.AttributeName,
                        Period1 = v.Period_1,
                        Period2 = v.Period_2,
                        Period3 = v.Period_3,
                        Period4 = v.Period_4,
                        Period5 = v.Period_5,
                        Period6 = v.Period_6,
                        Period7 = v.Period_7,
                        Period8 = v.Period_8,
                        Period9 = v.Period_9,
                        Period10 = v.Period_10,
                        Period11 = v.Period_11,
                        Period12 = v.Period_12
                    })
                    .ToListAsync(cancellationToken);
            }

            return values;
        }

        public async Task<IList<ESF_DPOutcome>> GetOutcomes(int ukPrn, CancellationToken cancellationToken)
        {
            IList<ESF_DPOutcome> values;

            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            using (var context = _validContext())
            {
                values = await context.ESF_DPOutcome
                    .Where(v => v.UKPRN == ukPrn)
                    .ToListAsync(cancellationToken);
            }

            return values;
        }
    }
}
