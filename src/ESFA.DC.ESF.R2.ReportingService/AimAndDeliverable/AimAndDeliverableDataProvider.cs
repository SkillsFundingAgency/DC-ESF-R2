using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable
{
    public class AimAndDeliverableDataProvider : IAimAndDeliverableDataProvider
    {
        private readonly IIlrDataProvider _ilrDataProvider;
        private readonly IFcsDataProvider _fcsDataProvider;
        private readonly ILarsDataProvider _larsDataProvider;

        public AimAndDeliverableDataProvider(
            IIlrDataProvider ilrDataProvider,
            IFcsDataProvider fcsDataProvider,
            ILarsDataProvider larsDataProvider)
        {
            _ilrDataProvider = ilrDataProvider;
            _fcsDataProvider = fcsDataProvider;
            _larsDataProvider = larsDataProvider;
        }

        public async Task<ICollection<ESFLearningDeliveryDeliverablePeriod>> GetDeliverablePeriodsAsync(int ukprn, CancellationToken cancellationToken)
        {
            return await _ilrDataProvider.GetDeliverablePeriodsAsync(ukprn, cancellationToken);
        }

        public async Task<ICollection<DPOutcome>> GetDpOutcomesAsync(int ukprn, CancellationToken cancellationToken)
        {
            return await _ilrDataProvider.GetDpOutcomesAsync(ukprn, cancellationToken);
        }

        public async Task<ICollection<ESFDPOutcome>> GetEsfDpOutcomesAsync(int ukprn, CancellationToken cancellationToken)
        {
            return await _ilrDataProvider.GetEsfDpOutcomesAsync(ukprn, cancellationToken);
        }

        public async Task<ICollection<FCSDeliverableCodeMapping>> GetFcsDeliverableCodeMappingsAsync(CancellationToken cancellationToken)
        {
            return await _fcsDataProvider.GetFcsDeliverableCodeMappingsAsync(cancellationToken);
        }

        public async Task<ICollection<LARSLearningDelivery>> GetLarsLearningDeliveriesAsync(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken)
        {
            return await _larsDataProvider.GetLarsLearningDeliveriesAsync(learnAimRefs, cancellationToken);
        }

        public async Task<ICollection<LearningDelivery>> GetLearningDeliveriesAsync(int ukprn, CancellationToken cancellationToken)
        {
            return await _ilrDataProvider.GetLearningDeliveriesAsync(ukprn, cancellationToken);
        }
    }
}
