using System.Collections.Generic;
using System.Threading;
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

        void PopulateDeliverableUnitCosts(
            List<string> newItemsNotInCache,
            int ukPrn,
            CancellationToken cancellationToken);

        IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            CancellationToken cancellationToken);

        void PopulateLarsLearningDeliveries(IEnumerable<string> uncached, CancellationToken cancellationToken);

        LarsLearningDeliveryModel GetLarsLearningDelivery(string learnAimRef);
    }
}