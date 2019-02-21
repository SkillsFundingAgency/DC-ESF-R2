using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef03 : IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;

        public LearnAimRef03(IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
        }

        public string ErrorName => "LearnAimRef_03";

        public bool IsWarning => false;

        public string ErrorMessage => "LearnAimRef does not exist on LARS.";

        public bool IsValid(SupplementaryDataModel model)
        {
            if (string.IsNullOrEmpty(model.LearnAimRef?.Trim()))
            {
                return true;
            }

            var larsLearningDelivery = _referenceDataService.GetLarsLearningDelivery(model.LearnAimRef);

            return larsLearningDelivery != null;
        }
    }
}