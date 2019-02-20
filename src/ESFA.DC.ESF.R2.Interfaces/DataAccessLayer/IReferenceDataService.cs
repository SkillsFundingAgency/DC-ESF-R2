using System.Collections.Generic;
using System.Threading;
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

        IList<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            CancellationToken cancellationToken);

        IEnumerable<string> GetLarsLearningDelivery(
            IEnumerable<string> learnAimRefs,
            CancellationToken cancellationToken);

        ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null);
    }
}