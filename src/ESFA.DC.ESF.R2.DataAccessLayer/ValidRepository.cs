using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ILR1819.DataStore.EF.Valid;
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

        public async Task<List<Learner>> GetLearners(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var learners = await _context.Learners
                .Where(l => l.UKPRN == ukPrn)
                .ToListAsync(cancellationToken);

            return learners;
        }

        public async Task<List<LearningDelivery>> GetLearningDeliveries(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var deliveries = await _context.LearningDeliveries
                .Where(l => l.UKPRN == ukPrn && l.FundModel == 70)
                .ToListAsync(cancellationToken);

            return deliveries;
        }

        public async Task<List<LearningDeliveryFAM>> GetLearningDeliveryFAMs(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var deliveryFams = await _context.LearningDeliveryFAMs
                .Where(l => l.UKPRN == ukPrn)
                .ToListAsync(cancellationToken);

            return deliveryFams;
        }

        public async Task<List<ProviderSpecLearnerMonitoring>> GetProviderSpecLearnerMonitorings(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var monitorings = await _context.ProviderSpecLearnerMonitorings
                .Where(l => l.UKPRN == ukPrn)
                .ToListAsync(cancellationToken);

            return monitorings;
        }

        public async Task<List<ProviderSpecDeliveryMonitoring>> GetProviderSpecDeliveryMonitorings(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var monitorings = await _context.ProviderSpecDeliveryMonitorings
                .Where(l => l.UKPRN == ukPrn)
                .ToListAsync(cancellationToken);

            return monitorings;
        }

        public async Task<List<DPOutcome>> GetDPOutcomes(int ukPrn, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var outcomes = await _context.DPOutcomes
                .Where(l => l.UKPRN == ukPrn)
                .ToListAsync(cancellationToken);

            return outcomes;
        }
    }
}