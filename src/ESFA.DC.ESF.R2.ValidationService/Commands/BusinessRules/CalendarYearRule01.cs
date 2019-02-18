﻿using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearRule01 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The CalendarYear is not valid";

        public string ErrorName => "CalendarYear_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return model.CalendarYear != null && model.CalendarYear >= 2016 && model.CalendarYear <= 2019;
        }
    }
}
