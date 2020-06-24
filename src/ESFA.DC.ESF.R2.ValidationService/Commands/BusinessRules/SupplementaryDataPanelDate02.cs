using System;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class SupplementaryDataPanelDate02 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly DateTime _ruleStartDate = new DateTime(2019, 4, 1);

        public SupplementaryDataPanelDate02(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.SupplementaryDataPanelDate_02;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            if (model?.SupplementaryDataPanelDate == null)
            {
                return true;
            }

            return model.SupplementaryDataPanelDate >= _ruleStartDate;
        }
    }
}