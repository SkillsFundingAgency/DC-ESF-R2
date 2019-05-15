using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ULNRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;

        public ULNRule02(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
        }

        public override string ErrorName => "ULN_02";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return model.ReferenceType != Constants.ReferenceType_LearnRefNumber ||
                      (model.ULN ?? 0) == 9999999999 ||
                      _referenceDataService.GetUlnLookup(new List<long?> { model.ULN ?? 0 }, CancellationToken.None).Any(u => u == model.ULN);
        }
    }
}
