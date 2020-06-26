using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class DeliverableCodeRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IFcsCodeMappingHelper _mappingHelper;

        public DeliverableCodeRule02(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService,
            IFcsCodeMappingHelper mappingHelper)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
            _mappingHelper = mappingHelper;
        }

        public override string ErrorName => RulenameConstants.DeliverableCode_02;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var fcsDeliverableCode = _mappingHelper.GetFcsDeliverableCode(model, CancellationToken.None);

            if (fcsDeliverableCode == 0)
            {
                return false;
            }

            var contractAllocation = _referenceDataService.GetContractAllocation(model.ConRefNumber, fcsDeliverableCode, CancellationToken.None);

            return contractAllocation != null;
        }
    }
}
