using System.Collections.Generic;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Services
{
    public class PopulationService : IPopulationService
    {
        private readonly IReferenceDataCache _cache;
        private readonly IFcsCodeMappingHelper _mappingHelper;

        public PopulationService(
            IReferenceDataCache cache,
            IFcsCodeMappingHelper mappingHelper)
        {
            _cache = cache;
            _mappingHelper = mappingHelper;
        }

        public void PrePopulateUlnCache(IList<long?> ulns, CancellationToken cancellationToken)
        {
            _cache.PopulateUlnLookup(ulns, cancellationToken);
        }

        public void PrePopulateContractDeliverableCodeMappings(IEnumerable<string> deliverableCodes, CancellationToken cancellationToken)
        {
            _cache.PopulateContractDeliverableCodeMappings(deliverableCodes, cancellationToken);
        }

        public void PrePopulateContractAllocations(int ukPrn, IList<SupplementaryDataModel> models, CancellationToken cancellationToken)
        {
            foreach (var model in models)
            {
                var fcsDeliverableCode = _mappingHelper.GetFcsDeliverableCode(model, cancellationToken);
                _cache.PopulateContractAllocations(model.ConRefNumber, fcsDeliverableCode, cancellationToken, ukPrn);
            }
        }

        public void PrePopulateContractDeliverableUnitCosts(int ukPrn, CancellationToken cancellationToken)
        {
            _cache.PopulateDeliverableUnitCosts(ESFConstants.UnitCostDeliverableCodes, ukPrn, cancellationToken);
        }
    }
}
