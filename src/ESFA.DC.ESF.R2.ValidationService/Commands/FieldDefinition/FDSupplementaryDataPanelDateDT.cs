using System;
using System.Globalization;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDSupplementaryDataPanelDateDT : BaseValidationRule, IFieldDefinitionValidator
    {
        public FDSupplementaryDataPanelDateDT(IValidationErrorMessageService errorMessageService)
            : base(errorMessageService)
        {
        }

        public override string ErrorName => RulenameConstants.FD_SupplementaryDataPanelDate_DT;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return string.IsNullOrEmpty(model.SupplementaryDataPanelDate)
                   || DateTime.TryParseExact(model.SupplementaryDataPanelDate, ValidationConstants.ShortDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}