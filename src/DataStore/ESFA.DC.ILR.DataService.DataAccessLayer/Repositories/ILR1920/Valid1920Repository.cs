using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.ILR1920EF.Valid;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using LearningDeliveryFam = ESFA.DC.ILR.DataService.Models.LearningDeliveryFam;
using ProviderSpecDeliveryMonitoring = ESFA.DC.ILR.DataService.Models.ProviderSpecDeliveryMonitoring;
using ProviderSpecLearnerMonitoring = ESFA.DC.ILR.DataService.Models.ProviderSpecLearnerMonitoring;

namespace ESFA.DC.ILR.DataService.DataAccessLayer.Repositories.ILR1920
{
    public class Valid1920Repository : IValid1920Repository
    {
        private readonly Func<ILR1920ValidLearnerContext> _context;

        public Valid1920Repository(
            Func<ILR1920ValidLearnerContext> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LearnerDetails>> GetLearnerDetails(
            int ukPrn,
            int fundingModel,
            IEnumerable<string> conRefNums,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var json = JsonConvert.SerializeObject(conRefNums);

            List<LearnerDetails> learnerDetails;
            using (var context = _context())
            {
                learnerDetails = await context.LearnerDetails
                    .FromSql($"Valid.GetLearnerDetails @UkPrn={ukPrn}, @fundModel={fundingModel}, @ConRefNumbers={json}")
                    .Select(entity => new LearnerDetails
                    {
                        Ukprn = entity.Ukprn,
                        Uln = entity.Uln,
                        LearnRefNumber = entity.LearnRefNumber,
                        AimSeqNumber = entity.AimSeqNumber,
                        ConRefNumber = entity.ConRefNumber,
                        LearnAimRef = entity.LearnAimRef,
                        Pmukprn = entity.Pmukprn,
                        FundModel = entity.FundModel,
                        PartnerUkprn = entity.PartnerUkprn,
                        CampId = entity.CampId,
                        CompStatus = entity.CompStatus,
                        LearnStartDate = entity.LearnStartDate,
                        LearnPlanEndDate = entity.LearnPlanEndDate,
                        LearnActEndDate = entity.LearnActEndDate,
                        DelLocPostCode = entity.DelLocPostCode,
                        Outcome = entity.Outcome,
                        AddHours = entity.AddHours,
                        SwsupAimId = entity.SwsupAimId
                    })
                    .ToListAsync(cancellationToken);
            }

            return learnerDetails;
        }

        public async Task<IEnumerable<string>> GetLearnerConRefNumbers(int ukPrn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<string> conRefNums;
            using (var context = _context())
            {
                conRefNums = await context.LearningDeliveries
                    .Where(ld => ld.Ukprn == ukPrn && ld.ConRefNumber != null)
                    .Select(ld => ld.ConRefNumber)
                    .Distinct()
                    .ToListAsync(cancellationToken);
            }

            return conRefNums;
        }

        public async Task<IEnumerable<LearningDeliveryFam>> GetLearningDeliveryFams(
            int ukPrn,
            string famType,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<LearningDeliveryFam> fams;

            using (var context = _context())
            {
                fams = await context.LearningDeliveryFams
                    .Where(l => l.Ukprn == ukPrn && l.LearnDelFamtype == famType)
                    .Select(ldf => new LearningDeliveryFam
                    {
                        LearnRefNumber = ldf.LearnRefNumber,
                        AimSeqNumber = ldf.AimSeqNumber,
                        LearnDelFamType = ldf.LearnDelFamtype,
                        LearnDelFamCode = ldf.LearnDelFamcode
                    }).ToListAsync(cancellationToken);
            }

            return fams;
        }

        public async Task<IEnumerable<ProviderSpecLearnerMonitoring>> GetLearnerMonitorings(int ukPrn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<ProviderSpecLearnerMonitoring> learnerMonitorings;

            using (var context = _context())
            {
                learnerMonitorings = await context.ProviderSpecLearnerMonitorings
                        .Where(m => m.Ukprn == ukPrn)
                        .Select(pslm => new ProviderSpecLearnerMonitoring
                        {
                            LearnRefNumber = pslm.LearnRefNumber,
                            ProvSpecLearnMon = pslm.ProvSpecLearnMon,
                            ProvSpecLearnMonOccur = pslm.ProvSpecLearnMonOccur
                        }).ToListAsync(cancellationToken);
            }

            return learnerMonitorings;
        }

        public async Task<IEnumerable<ProviderSpecDeliveryMonitoring>> GetDeliveryMonitorings(int ukPrn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            IEnumerable<ProviderSpecDeliveryMonitoring> learnerMonitorings;

            using (var context = _context())
            {
                learnerMonitorings = await context.ProviderSpecDeliveryMonitorings
                    .Where(m => m.Ukprn == ukPrn)
                    .Select(psdm => new ProviderSpecDeliveryMonitoring
                    {
                        LearnRefNumber = psdm.LearnRefNumber,
                        AimSeqNumber = psdm.AimSeqNumber,
                        ProvSpecDelMon = psdm.ProvSpecDelMon,
                        ProvSpecDelMonOccur = psdm.ProvSpecDelMonOccur
                    }).ToListAsync(cancellationToken);
            }

            return learnerMonitorings;
        }

        public async Task<IEnumerable<DpOutcome>> GetDPOutcomes(int ukPrn, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            List<DpOutcome> outcomes;
            using (var context = _context())
            {
                outcomes = await context.Dpoutcomes
                    .Where(l => l.Ukprn == ukPrn)
                    .Select(dpo => new DpOutcome
                    {
                        UkPrn = dpo.Ukprn,
                        LearnRefNumber = dpo.LearnRefNumber,
                        OutType = dpo.OutType,
                        OutCode = dpo.OutCode,
                        OutStartDate = dpo.OutStartDate,
                        OutEndDate = dpo.OutEndDate,
                        OutCollDate = dpo.OutCollDate
                    })
                    .ToListAsync(cancellationToken);
            }

            return outcomes;
        }
    }
}