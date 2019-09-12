using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IPopulationService
    {
        void PrePopulateUlnCache(IList<long?> ulns, CancellationToken cancellationToken);

        void PrePopulateContractAllocations(int ukPrn, IList<SupplementaryDataModel> models, CancellationToken cancellationToken);

        void PrePopulateContractDeliverableUnitCosts(int ukPrn, CancellationToken cancellationToken);

        void PrePopulateContractDeliverableCodeMappings(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken);

        Task PrePopulateLarsLearningDeliveries(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken);

        Task PrePopulateValidationErrorMessages(CancellationToken cancellationToken);
    }
}