using System.Collections.Generic;
using System.Threading;
using ESFA.DC.Data.LARS.Model;
using ESFA.DC.Data.ULN.Model;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ReferenceData.FCS.Model;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IReferenceDataCache
    {
        List<UniqueLearnerNumber> Ulns { get; }

        List<ContractDeliverableCodeMapping> CodeMappings { get; }

        List<ContractAllocationCacheModel> ContractAllocations { get; }

        List<DeliverableUnitCost> DeliverableUnitCosts { get; }

        IDictionary<int, string> ProviderNameByUkprn { get; }

        List<LARS_LearningDelivery> LarsLearningDeliveries { get; }

        int CurrentPeriod { get; set; }

        string GetProviderName(
            int ukPrn,
            CancellationToken cancellationToken);

        IList<ContractDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IList<string> deliverableCodes,
            CancellationToken cancellationToken);

        IList<UniqueLearnerNumber> GetUlnLookup(
            IList<long?> searchUlns,
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

        IList<LARS_LearningDelivery> GetLarsLearningDelivery(
            IList<string> learnAimRefs,
            CancellationToken cancellationToken);

        string GetPostcodeVersion(CancellationToken cancellationToken);

        string GetLarsVersion(CancellationToken cancellationToken);

        string GetOrganisationVersion(CancellationToken cancellationToken);
    }
}