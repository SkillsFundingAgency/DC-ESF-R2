using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataService
    {
        int CurrentPeriod { get; set; }

        string GetPostcodeVersion(CancellationToken cancellationToken);

        string GetLarsVersion(CancellationToken cancellationToken);

        string GetOrganisationVersion(CancellationToken cancellationToken);

        string GetProviderName(int ukPrn, CancellationToken cancellationToken);

        IEnumerable<long> GetUlnLookup(IEnumerable<long?> searchUlns, CancellationToken cancellationToken);

        IList<DeliverableUnitCost> GetDeliverableUnitCosts(string conRefNum, IList<string> deliverableCodes);

        decimal? GetDeliverableUnitCostForDeliverableCode(string conRefNum, string deliverableCode);

        LarsLearningDeliveryModel GetLarsLearningDelivery(string learnAimRef);

        ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null);

        IEnumerable<FcsDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken);

        Task<IEnumerable<LarsLearningDeliveryModel>> GetLarsLearningDelivery(IEnumerable<string> learnAimRefs, CancellationToken cancellationToken);

        Task<IEnumerable<string>> GetContractAllocationsForUkprn(int ukprn, CancellationToken cancellationToken);
    }
}