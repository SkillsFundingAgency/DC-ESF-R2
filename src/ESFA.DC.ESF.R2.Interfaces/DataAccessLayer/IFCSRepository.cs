using System.Collections.Generic;
using System.Threading;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ReferenceData.FCS.Model;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IFCSRepository
    {
        IList<ContractDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IList<string> deliverableCodes,
            CancellationToken cancellationToken);

        ContractAllocationCacheModel GetContractAllocation(
            string conRefNum,
            int deliverableCode,
            CancellationToken cancellationToken,
            int? ukPrn = null);

        IEnumerable<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            string conRefNum,
            CancellationToken cancellationToken);
    }
}