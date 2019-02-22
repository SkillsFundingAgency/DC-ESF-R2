using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Utils;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef06 : IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;

        private readonly List<string> _acceptedGenres = new List<string>
        {
            Constants.LarsLearningDeliveryGenre_EOQ,
            Constants.LarsLearningDeliveryGenre_EQQ,
            Constants.LarsLearningDeliveryGenre_EOU,
            Constants.LarsLearningDeliveryGenre_IHE
        };

        public LearnAimRef06(IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
        }

        public string ErrorName => "LearnAimRef_06";

        public bool IsWarning => false;

        public string ErrorMessage => "LearnAimRef must be a non-regulated aim on LARS for the selected DeliverableCode.";

        public bool IsValid(SupplementaryDataModel model)
        {
            if (string.IsNullOrWhiteSpace(model.LearnAimRef)
                || !(model.DeliverableCode ?? string.Empty).CaseInsensitiveEquals(Constants.DeliverableCode_NR01))
            {
                return true;
            }

            var larsLearningDelivery = _referenceDataService.GetLarsLearningDelivery(model.LearnAimRef);

            return larsLearningDelivery == null
                   || _acceptedGenres.All(ag => !ag.CaseInsensitiveEquals(larsLearningDelivery.LearningDeliveryGenre));
        }
    }
}