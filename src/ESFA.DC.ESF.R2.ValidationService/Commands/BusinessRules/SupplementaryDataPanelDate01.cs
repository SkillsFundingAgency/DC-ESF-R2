using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class SupplementaryDataPanelDate01 : IBusinessRuleValidator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public SupplementaryDataPanelDate01(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public string ErrorName => "SupplementaryDataPanelDate_01";

        public bool IsWarning => false;

        public string ErrorMessage => "The SupplementaryDataPanelDate cannot be in the future.";

        public bool IsValid(SupplementaryDataModel model)
        {
            if (model?.SupplementaryDataPanelDate == null)
            {
                return true;
            }

            return model.SupplementaryDataPanelDate <= _dateTimeProvider.ConvertUtcToUk(_dateTimeProvider.GetNowUtc());
        }
    }
}