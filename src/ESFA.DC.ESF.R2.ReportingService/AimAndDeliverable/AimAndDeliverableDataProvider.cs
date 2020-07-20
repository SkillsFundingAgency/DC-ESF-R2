using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable;
using ESFA.DC.ESF.R2.Models.AimAndDeliverable;
using IIlrDataProvider1920 = ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable.DataProvider._1920;
using IIlrDataProvider2021 = ESFA.DC.ESF.R2.Interfaces.Reports.AimAndDeliverable.DataProvider._2021;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable
{
    public class AimAndDeliverableDataProvider : IAimAndDeliverableDataProvider
    {
        private readonly IIlrDataProvider1920.IIlrDataProvider _ilrDataProvider1920;
        private readonly IIlrDataProvider2021.IIlrDataProvider _ilrDataProvider2021;
        private readonly IFcsDataProvider _fcsDataProvider;
        private readonly ILarsDataProvider _larsDataProvider;

        public AimAndDeliverableDataProvider(
            IIlrDataProvider1920.IIlrDataProvider ilrDataProvider1920,
            IIlrDataProvider2021.IIlrDataProvider ilrDataProvider2021,
            IFcsDataProvider fcsDataProvider,
            ILarsDataProvider larsDataProvider)
        {
            _ilrDataProvider1920 = ilrDataProvider1920;
            _ilrDataProvider2021 = ilrDataProvider2021;
            _fcsDataProvider = fcsDataProvider;
            _larsDataProvider = larsDataProvider;
        }

        public async Task<ICollection<ESFLearningDeliveryDeliverablePeriod>> GetDeliverablePeriodsAsync(int ukprn, CancellationToken cancellationToken)
        {
            var result = new List<ESFLearningDeliveryDeliverablePeriod>();

            var ilr1920Data = _ilrDataProvider1920.GetDeliverablePeriodsAsync(ukprn, cancellationToken);

            var ilr2021Data = _ilrDataProvider2021.GetDeliverablePeriodsAsync(ukprn, cancellationToken);

            await Task.WhenAll(ilr1920Data, ilr2021Data);

            result.AddRange(ilr1920Data.Result);

            result.AddRange(ilr2021Data.Result);

            return result;
        }

        public async Task<ICollection<DPOutcome>> GetDpOutcomesAsync(int ukprn, CancellationToken cancellationToken)
        {
            var result = new List<DPOutcome>();

            var ilr1920Data = _ilrDataProvider1920.GetDpOutcomesAsync(ukprn, cancellationToken);

            var ilr2021Data = _ilrDataProvider2021.GetDpOutcomesAsync(ukprn, cancellationToken);

            await Task.WhenAll(ilr1920Data, ilr2021Data);

            result.AddRange(ilr1920Data.Result);

            result.AddRange(ilr2021Data.Result);

            return result;
        }

        public async Task<ICollection<ESFDPOutcome>> GetEsfDpOutcomesAsync(int ukprn, CancellationToken cancellationToken)
        {
            var result = new List<ESFDPOutcome>();

            var ilr1920Data = _ilrDataProvider1920.GetEsfDpOutcomesAsync(ukprn, cancellationToken);

            var ilr2021Data = _ilrDataProvider2021.GetEsfDpOutcomesAsync(ukprn, cancellationToken);

            await Task.WhenAll(ilr1920Data, ilr2021Data);

            result.AddRange(ilr1920Data.Result);

            result.AddRange(ilr2021Data.Result);

            return result;
        }

        public async Task<ICollection<FCSDeliverableCodeMapping>> GetFcsDeliverableCodeMappingsAsync(CancellationToken cancellationToken)
        {
            return await _fcsDataProvider.GetFcsDeliverableCodeMappingsAsync(cancellationToken);
        }

        public async Task<ICollection<LARSLearningDelivery>> GetLarsLearningDeliveriesAsync(ICollection<LearningDelivery> learningDeliveries, CancellationToken cancellationToken)
        {
            return await _larsDataProvider.GetLarsLearningDeliveriesAsync(learningDeliveries, cancellationToken);
        }

        public async Task<ICollection<LearningDelivery>> GetLearningDeliveriesAsync(int ukprn, CancellationToken cancellationToken)
        {
            var result = new List<LearningDelivery>();

            var ilr1920Data = _ilrDataProvider1920.GetLearningDeliveriesAsync(ukprn, cancellationToken);

            var ilr2021Data = _ilrDataProvider2021.GetLearningDeliveriesAsync(ukprn, cancellationToken);

            await Task.WhenAll(ilr1920Data, ilr2021Data);

            result.AddRange(ilr1920Data.Result);

            result.AddRange(ilr2021Data.Result);

            return result;
        }
    }
}
