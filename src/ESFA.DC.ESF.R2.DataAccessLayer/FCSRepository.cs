using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.Logging.Interfaces;
using ESFA.DC.ReferenceData.FCS.Model;
using ESFA.DC.ReferenceData.FCS.Model.Interface;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class FCSRepository : IFCSRepository
    {
        private readonly Func<IFcsContext> _fcsContextFactory;
        private readonly ILogger _logger;

        private readonly object _fcsContextLock = new object();

        public FCSRepository(
            Func<IFcsContext> fcsContextFactory,
            ILogger logger)
        {
            _logger = logger;
            _fcsContextFactory = fcsContextFactory;
        }

        public IList<ContractDeliverableCodeMapping> GetContractDeliverableCodeMapping(IList<string> deliverableCodes, CancellationToken cancellationToken)
        {
            List<ContractDeliverableCodeMapping> codeMapping = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                lock (_fcsContextLock)
                {
                    using (var fcsContext = _fcsContextFactory())
                    {
                        codeMapping = fcsContext.ContractDeliverableCodeMappings
                            .Where(x => deliverableCodes.Any(dc => dc.CaseInsensitiveEquals(x.ExternalDeliverableCode)))
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get FCS ContractDeliverableCodeMapping", ex);
            }

            return codeMapping;
        }

        public ContractAllocationCacheModel GetContractAllocation(string conRefNum, int deliverableCode, CancellationToken cancellationToken, int? ukPrn = null)
        {
            ContractAllocationCacheModel contractAllocationModel = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                lock (_fcsContextLock)
                {
                    using (var fcsContext = _fcsContextFactory())
                    {
                        contractAllocationModel = fcsContext.ContractAllocations
                            .Where(ca => ca.DeliveryUkprn == ukPrn
                                         && ca.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum)
                                         && ca.ContractDeliverables.Any(cd => cd.DeliverableCode == deliverableCode))
                            .Select(ca => new ContractAllocationCacheModel
                            {
                                DeliverableCode = deliverableCode,
                                ContractAllocationNumber = ca.ContractAllocationNumber,
                                StartDate = ca.StartDate,
                                EndDate = ca.EndDate
                            })
                            .FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get FCS ContractDeliverableCodeMapping", ex);
            }

            return contractAllocationModel;
        }

        public IEnumerable<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            string conRefNum,
            CancellationToken cancellationToken)
        {
            List<ContractDeliverableCodeMapping> mappings = GetContractDeliverableCodeMapping(deliverableCodes, cancellationToken).ToList();

            List<DeliverableUnitCost> deliverableUnitCosts = null;
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }

                lock (_fcsContextLock)
                {
                    using (var fcsContext = _fcsContextFactory())
                    {
                        deliverableUnitCosts = fcsContext.ContractDeliverables
                            .Join(mappings, cd => (cd.DeliverableCode ?? 0).ToString(), m => m.FcsdeliverableCode,
                                (cd, m) => cd)
                            .Where(cd => cd.ContractAllocation.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum)
                                         && cd.ContractAllocation.DeliveryUkprn == ukPrn)
                            .Select(cd => new DeliverableUnitCost
                            {
                                UkPrn = ukPrn,
                                ConRefNum = conRefNum,
                                DeliverableCode = mappings
                                    .Where(m => m.FcsdeliverableCode == (cd.DeliverableCode ?? 0).ToString())
                                    .Select(m => m.ExternalDeliverableCode)
                                    .FirstOrDefault(),
                                UnitCost = cd.UnitCost ?? 0
                            })
                            .ToList();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to get FCS ContractDeliverable unit costs", ex);
            }

            return deliverableUnitCosts;
        }
    }
}