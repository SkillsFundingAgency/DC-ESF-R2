using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models.Ilr;
using ESFA.DC.ILR1819.DataStore.EF.Valid.Interfaces;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class ValidRepository : IValidRepository
    {
        private readonly IILR1819_DataStoreEntitiesValid _context;

        public ValidRepository(
            IILR1819_DataStoreEntitiesValid context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LearnerModel>> GetValidLearnerData(
            int ukPrn,
            string conRefNum,
            CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var learners = await _context
                .Learners
                .Where(l => l.UKPRN == ukPrn && l.LearningDeliveries.Any(ld => ld.ConRefNumber == conRefNum))
                .Select(
                    l => new LearnerModel
                    {
                        UkPrn = l.UKPRN,
                        Uln = l.ULN,
                        LearnRefNumber = l.LearnRefNumber,
                        CampId = l.CampId,
                        PmUkPrn = l.PMUKPRN,
                        LearningDeliveries = l.LearningDeliveries
                            .Select(ld => new LearningDeliveryModel
                        {
                            ConRefNum = ld.ConRefNumber,
                            LearnRefNumber = ld.LearnRefNumber,
                            LearnAimRef = ld.LearnAimRef,
                            AimSequenceNumber = ld.AimSeqNumber,
                            FundModel = ld.FundModel,
                            LearnStartDate = ld.LearnStartDate,
                            LearnPlanEndDate = ld.LearnPlanEndDate,
                            LearnActEndDate = ld.LearnActEndDate,
                            CompStatus = ld.CompStatus,
                            DelLocPostCode = ld.DelLocPostCode,
                            Outcome = ld.Outcome,
                            AddHours = ld.AddHours,
                            PartnerUkPrn = ld.PartnerUKPRN,
                            SwSupAimId = ld.SWSupAimId,
                            LearningDeliveryFams = ld.LearningDeliveryFAMs
                                .Select(ldf => new LearningDeliveryFamModel
                            {
                                LearnDelFamType = ldf.LearnDelFAMType,
                                LearnDelFamCode = ldf.LearnDelFAMCode
                            }),
                            ProviderSpecDeliveryMonitorings = ld.ProviderSpecDeliveryMonitorings
                                .Select(psdm => new ProviderSpecDeliveryMonitoringModel
                            {
                                ProvSpecDelMonOccur = psdm.ProvSpecDelMonOccur,
                                ProvSpecDelMon = psdm.ProvSpecDelMon
                            })
                        }),
                        ProviderSpecLearnerMonitorings = l.ProviderSpecLearnerMonitorings
                            .Select(pslm => new ProviderSpecLearnerMonitoringModel
                            {
                                ProvSpecLearnMonOccur = pslm.ProvSpecLearnMonOccur,
                                ProvSpecLearnMon = pslm.ProvSpecLearnMon
                            })
                    }).ToListAsync(cancellationToken);

            return learners;
        }

        public async Task<IEnumerable<DpOutcomeModel>> GetDPOutcomes(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var outcomes = await _context.DPOutcomes
                .Where(l => l.UKPRN == ukPrn)
                .Select(dpo => new DpOutcomeModel
                {
                    UkPrn = dpo.UKPRN,
                    OutType = dpo.OutType,
                    OutCode = dpo.OutCode,
                    OutStartDate = dpo.OutStartDate,
                    OutEndDate = dpo.OutEndDate,
                    OutCollDate = dpo.OutCollDate
                })
                .ToListAsync(cancellationToken);

            return outcomes;
        }
    }
}