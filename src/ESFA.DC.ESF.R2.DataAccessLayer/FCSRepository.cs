using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.Models.Validation;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ReferenceData.FCS.Model.Interface;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class FCSRepository : IFCSRepository
    {
        private readonly Func<IFcsContext> _fcsContextFactory;

        private readonly object _fcsContextLock = new object();

        public FCSRepository(
            Func<IFcsContext> fcsContextFactory)
        {
            _fcsContextFactory = fcsContextFactory;
        }

        public IEnumerable<FcsDeliverableCodeMapping> GetContractDeliverableCodeMapping(
            IEnumerable<string> deliverableCodes,
            CancellationToken cancellationToken)
        {
            List<FcsDeliverableCodeMapping> codeMapping;

            cancellationToken.ThrowIfCancellationRequested();

            lock (_fcsContextLock)
            {
                using (var fcsContext = _fcsContextFactory())
                {
                    codeMapping = fcsContext.ContractDeliverableCodeMappings
                        .Where(x => deliverableCodes.Any(dc => dc.CaseInsensitiveEquals(x.ExternalDeliverableCode)))
                        .Select(x => new FcsDeliverableCodeMapping
                            {
                                FundingStreamPeriodCode = x.FundingStreamPeriodCode,
                                FcsDeliverableCode = x.FcsdeliverableCode,
                                ExternalDeliverableCode = x.ExternalDeliverableCode,
                                DeliverableName = x.DeliverableName
                            })
                        .ToList();
                }
            }

            return codeMapping;
        }

        public ContractAllocationCacheModel GetContractAllocation(string conRefNum, int deliverableCode, CancellationToken cancellationToken, int? ukPrn = null)
        {
            ContractAllocationCacheModel contractAllocationModel;

            cancellationToken.ThrowIfCancellationRequested();

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

            return contractAllocationModel;
        }

        public IEnumerable<DeliverableUnitCost> GetDeliverableUnitCosts(
            IList<string> deliverableCodes,
            int ukPrn,
            string conRefNum,
            CancellationToken cancellationToken)
        {
            List<FcsDeliverableCodeMapping> mappings = GetContractDeliverableCodeMapping(deliverableCodes, cancellationToken).ToList();

            List<DeliverableUnitCost> deliverableUnitCosts;

            cancellationToken.ThrowIfCancellationRequested();

            lock (_fcsContextLock)
            {
                using (var fcsContext = _fcsContextFactory())
                {
                    deliverableUnitCosts = fcsContext.ContractDeliverables
                        .Where(cd => cd.ContractAllocation.ContractAllocationNumber.CaseInsensitiveEquals(conRefNum)
                                     && cd.ContractAllocation.DeliveryUkprn == ukPrn)
                        .Join(
                            mappings,
                            cd => (cd.DeliverableCode ?? 0).ToString(),
                            m => m.FcsDeliverableCode,
                            (cd, m) => new DeliverableUnitCost
                            {
                                UkPrn = ukPrn,
                                ConRefNum = conRefNum,
                                DeliverableCode = m.ExternalDeliverableCode,
                                UnitCost = cd.UnitCost ?? 0
                            })
                        .ToList();
                }
            }

            return deliverableUnitCosts;
        }
    }
}