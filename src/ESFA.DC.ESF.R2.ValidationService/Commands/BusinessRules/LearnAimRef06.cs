using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef06 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;

        private readonly List<string> _acceptedGenres = new List<string>
        {
            ValidationConstants.LarsLearningDeliveryGenre_EOQ,
            ValidationConstants.LarsLearningDeliveryGenre_EQQ,
            ValidationConstants.LarsLearningDeliveryGenre_EOU,
            ValidationConstants.LarsLearningDeliveryGenre_IHE
        };

        public LearnAimRef06(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
        }

        public override string ErrorName => RulenameConstants.LearnAimRef_06;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LearnAimRef)
                || !(model.DeliverableCode ?? string.Empty).CaseInsensitiveEquals(DeliverableCodeConstants.DeliverableCode_NR01))
            {
                return true;
            }

            var larsLearningDelivery = _referenceDataService.GetLarsLearningDelivery(model.LearnAimRef);

            return larsLearningDelivery == null
                   || _acceptedGenres.All(ag => !ag.CaseInsensitiveEquals(larsLearningDelivery.LearningDeliveryGenre));
        }
    }
}