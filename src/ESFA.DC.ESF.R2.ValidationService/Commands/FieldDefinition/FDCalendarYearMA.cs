﻿using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.FieldDefinition
{
    public class FDCalendarYearMA : IFieldDefinitionValidator
    {
        public string ErrorName => "FD_CalendarYear_MA";

        public bool IsWarning => false;

        public string ErrorMessage =>
            "The DeliverableCode is mandatory. Please resubmit the file including the appropriate value.";

        public bool Execute(SupplementaryDataLooseModel model)
        {
            return !string.IsNullOrEmpty(model.CalendarYear?.Trim());
        }
    }
}
