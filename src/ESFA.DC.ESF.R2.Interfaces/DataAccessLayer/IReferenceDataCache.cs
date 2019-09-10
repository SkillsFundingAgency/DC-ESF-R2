using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataCache
    {
        string GetProviderName(
            int ukPrn,
            CancellationToken cancellationToken);

        void PopulateContractDeliverableCodeMappings(IEnumerable<string> uncached, CancellationToken cancellationToken);

        void PopulateContractDeliverableCodeMappings(IEnumerable<FcsDeliverableCodeMapping> mappings);

        IEnumerable<FcsDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken);

        void PopulateUlnLookup(IEnumerable<long?> unknownUlns, CancellationToken cancellationToken);

        IEnumerable<long> GetUlnLookup(
            IEnumerable<long?> searchUlns,
            CancellationToken cancellationToken);

        void PopulateContractAllocations(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn);

        ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null);

        Task PopulateDeliverableUnitCosts(
            List<string> newItemsNotInCache,
            int ukPrn,
            CancellationToken cancellationToken);

        IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            string conRefNum,
            IList<string> deliverableCodes);

        Task PopulateLarsLearningDeliveries(IEnumerable<string> uncached, CancellationToken cancellationToken);

        LarsLearningDeliveryModel GetLarsLearningDelivery(string learnAimRef);

        IEnumerable<LarsLearningDeliveryModel> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs);
    }
}