using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Services
{
    public class Fm70DataService : IFm70DataService
    {
        private readonly IFm701516Repository _repository1516;
        private readonly IFm701617Repository _repository1617;
        private readonly IFm701718Repository _repository1718;
        private readonly IFm701819Repository _repository1819;
        private readonly IFm701920Repository _repository1920;

        public Fm70DataService(
            IFm701516Repository repository1516,
            IFm701617Repository repository1617,
            IFm701718Repository repository1718,
            IFm701819Repository repository1819,
            IFm701920Repository repository1920)
        {
            _repository1516 = repository1516;
            _repository1617 = repository1617;
            _repository1718 = repository1718;
            _repository1819 = repository1819;
            _repository1920 = repository1920;
        }

        public async Task<IEnumerable<Fm70LearningDelivery>> GetLearningDeliveries(int ukPrn, bool round2, CancellationToken cancellationToken)
        {
            if (round2)
            {
                return (await _repository1920.GetLearningDeliveries(ukPrn, cancellationToken)).ToList();
            }

            return (await _repository1819.GetLearningDeliveries(ukPrn, cancellationToken)).ToList();
        }

        public async Task<IEnumerable<FM70PeriodisedValues>> GetPeriodisedValuesAllYears(
            int ukPrn,
            CancellationToken cancellationToken,
            bool round2 = false)
        {
            var result = new List<FM70PeriodisedValues>();

            List<FM70PeriodisedValues> periodisedValues1516 = null;
            List<FM70PeriodisedValues> periodisedValues1617 = null;
            List<FM70PeriodisedValues> periodisedValues1718 = null;

            List<FM70PeriodisedValues> periodisedValues1920 = null;

            var periodisedValues1819 = (await _repository1819.GetPeriodisedValues(ukPrn, cancellationToken)).ToList();

            if (!round2)
            {
                periodisedValues1516 = (await _repository1516.Get1516PeriodisedValues(ukPrn, cancellationToken)).ToList();
                periodisedValues1617 = (await _repository1617.Get1617PeriodisedValues(ukPrn, cancellationToken)).ToList();
                periodisedValues1718 = (await _repository1718.Get1718PeriodisedValues(ukPrn, cancellationToken)).ToList();
            }
            else
            {
                periodisedValues1920 = (await _repository1920.GetPeriodisedValues(ukPrn, cancellationToken)).ToList();
            }

            if (periodisedValues1516?.Any() ?? false)
            {
                result.AddRange(periodisedValues1516);
            }

            if (periodisedValues1617?.Any() ?? false)
            {
                result.AddRange(periodisedValues1617);
            }

            if (periodisedValues1718?.Any() ?? false)
            {
                result.AddRange(periodisedValues1718);
            }

            if (periodisedValues1819.Any())
            {
                result.AddRange(periodisedValues1819);
            }

            if (periodisedValues1920?.Any() ?? false)
            {
                result.AddRange(periodisedValues1920);
            }

            return result;
        }

        public async Task<IEnumerable<Fm70DpOutcome>> GetOutcomes(int ukPrn, CancellationToken cancellationToken, bool round2 = false)
        {
            var dpOutcomes = new List<Fm70DpOutcome>();

            List<Fm70DpOutcome> outcomes1819 = null;
            List<Fm70DpOutcome> outcomes1920 = null;

            if (round2)
            {
                outcomes1920 = (await _repository1920.GetOutcomes(ukPrn, cancellationToken)).ToList();
            }
            else
            {
                outcomes1819 = (await _repository1819.GetOutcomes(ukPrn, cancellationToken)).ToList();
            }

            if (outcomes1819?.Any() ?? false)
            {
                dpOutcomes.AddRange(outcomes1819);
            }

            if (outcomes1920?.Any() ?? false)
            {
                dpOutcomes.AddRange(outcomes1920);
            }

            return dpOutcomes;
        }
    }
}