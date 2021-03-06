﻿using System;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearCalendarMonthRule03 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IFcsCodeMappingHelper _mappingHelper;

        public CalendarYearCalendarMonthRule03(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService,
            IFcsCodeMappingHelper mappingHelper)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
            _mappingHelper = mappingHelper;
        }

        public override string ErrorName => RulenameConstants.CalendarYearCalendarMonth_03;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var year = model.CalendarYear ?? 0;
            var month = model.CalendarMonth ?? 0;

            if (year == 0 || month == 0 || month < ValidationConstants.CalendarMonthMinValue || month > ValidationConstants.CalendarMonthMaxValue)
            {
                return false;
            }

            var startDateMonth = new DateTime(year, month, 1);

            var fcsDeliverableCode = _mappingHelper.GetFcsDeliverableCode(model, CancellationToken.None);
            var contractAllocation = _referenceDataService.GetContractAllocation(model.ConRefNumber, fcsDeliverableCode, CancellationToken.None);

            return contractAllocation == null || contractAllocation.EndDate > startDateMonth;
        }
    }
}
