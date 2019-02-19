using System.Collections.Generic;
using System.Threading;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataCache
    {
        HashSet<long> Ulns { get; }

        List<FcsDeliverableCodeMapping> CodeMappings { get; }

        List<ContractAllocationCacheModel> ContractAllocations { get; }

        List<DeliverableUnitCost> DeliverableUnitCosts { get; }

        IDictionary<int, string> ProviderNameByUkprn { get; }

        HashSet<string> LarsLearnAimRefs { get; }

        int CurrentPeriod { get; set; }

        string GetProviderName(
            int ukPrn,
            CancellationToken cancellationToken);

        IEnumerable<FcsDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken);

        IEnumerable<long> GetUlnLookup(
            IEnumerable<long?> searchUlns,
            CancellationToken cancellationToken);

        ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null);

        IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            CancellationToken cancellationToken);

        IEnumerable<string> GetLarsLearningDelivery(
            IEnumerable<string> learnAimRefs,
            CancellationToken cancellationToken);

        string GetPostcodeVersion(CancellationToken cancellationToken);

        string GetLarsVersion(CancellationToken cancellationToken);

        string GetOrganisationVersion(CancellationToken cancellationToken);
    }
}