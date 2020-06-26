using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class SupplementaryDataPanelDate01 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public SupplementaryDataPanelDate01(
            IValidationErrorMessageService errorMessageService,
            IDateTimeProvider dateTimeProvider)
            : base(errorMessageService)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public override string ErrorName => RulenameConstants.SupplementaryDataPanelDate_01;

        public bool IsWarning => false;

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