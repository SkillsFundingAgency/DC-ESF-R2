using System;
using System.Globalization;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDSupplementaryDataPanelDateDT : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_SupplementaryDataPanelDate_DT";

        public bool IsWarning => false;

        public string ErrorMessage => "SupplementaryDataPanelDate must be a date in the format DD/MM/YYYY.  Please adjust the value and resubmit the file.";

        public bool IsValid(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.SupplementaryDataPanelDate)
                   && DateTime.TryParseExact(model.SupplementaryDataPanelDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
    }
}