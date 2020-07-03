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
        private readonly IFm701819Repository _repository1819;
        private readonly IFm701920Repository _repository1920;

        public Fm70DataService(
            IFm701819Repository repository1819,
            IFm701920Repository repository1920)
        {
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

            var periodisedValues1819 = (await _repository1819.GetPeriodisedValues(ukPrn, cancellationToken)).ToList();

            var periodisedValues1920 = (await _repository1920.GetPeriodisedValues(ukPrn, cancellationToken)).ToList();

            if (periodisedValues1819.Any())
            {
                result.AddRange(periodisedValues1819);
            }

            if (periodisedValues1920.Any())
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