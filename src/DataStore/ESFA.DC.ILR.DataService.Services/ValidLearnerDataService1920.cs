using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Interfaces.Repositories;
using ESFA.DC.ILR.DataService.Interfaces.Services;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ILR.DataService.Services
{
    public class ValidLearnerDataService1920 : IValidLearnerDataService1920
    {
        private readonly IValid1920Repository _validLearnerRepository;

        public ValidLearnerDataService1920(
            IValid1920Repository validLearnerRepository)
        {
            _validLearnerRepository = validLearnerRepository;
        }

        public async Task<IEnumerable<LearnerDetails>> GetLearnerDetails(
            int ukPrn,
            int fundingModel,
            IEnumerable<string> conRefNums,
            CancellationToken cancellationToken)
        {
            var learnerDetails = (await _validLearnerRepository.GetLearnerDetails(ukPrn, fundingModel, conRefNums, cancellationToken))
                .ToList();

            return learnerDetails;
        }

        public async Task<IEnumerable<string>> GetLearnerConRefNumbers(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var conRefNumbers = (await _validLearnerRepository.GetLearnerConRefNumbers(ukPrn, cancellationToken)).ToList();

            return conRefNumbers;
        }

        public async Task<IEnumerable<DpOutcome>> GetDPOutcomes(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var outcomes = (await _validLearnerRepository.GetDPOutcomes(ukPrn, cancellationToken)).ToList();

            return outcomes;
        }

        public async Task<IEnumerable<LearningDeliveryFam>> GetLearningDeliveryFams(
            int ukPrn,
            string famType,
            CancellationToken cancellationToken)
        {
            var fams = await _validLearnerRepository.GetLearningDeliveryFams(ukPrn, famType, cancellationToken);

            return fams;
        }

        public async Task<IEnumerable<ProviderSpecLearnerMonitoring>> GetLearnerMonitorings(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var monitorings = await _validLearnerRepository.GetLearnerMonitorings(ukPrn, cancellationToken);

            return monitorings;
        }

        public async Task<IEnumerable<ProviderSpecDeliveryMonitoring>> GetDeliveryMonitorings(
            int ukPrn,
            CancellationToken cancellationToken)
        {
            var monitorings = await _validLearnerRepository.GetDeliveryMonitorings(ukPrn, cancellationToken);

            return monitorings;
        }
    }
}