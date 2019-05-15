using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef05 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;

        private readonly List<string> _acceptedGenres = new List<string>
        {
            Constants.LarsLearningDeliveryGenre_EOQ,
            Constants.LarsLearningDeliveryGenre_EQQ,
            Constants.LarsLearningDeliveryGenre_EOU,
            Constants.LarsLearningDeliveryGenre_IHE
        };

        public LearnAimRef05(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
        }

        public override string ErrorName => "LearnAimRef_05";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            if (string.IsNullOrEmpty(model.LearnAimRef?.Trim())
                || !(model.DeliverableCode ?? string.Empty).CaseInsensitiveEquals(Constants.DeliverableCode_RQ01))
            {
                return true;
            }

            var larsLearningDelivery = _referenceDataService.GetLarsLearningDelivery(model.LearnAimRef);

            return larsLearningDelivery == null
                   || _acceptedGenres.Any(ag => ag.CaseInsensitiveEquals(larsLearningDelivery.LearningDeliveryGenre));
        }
    }
}