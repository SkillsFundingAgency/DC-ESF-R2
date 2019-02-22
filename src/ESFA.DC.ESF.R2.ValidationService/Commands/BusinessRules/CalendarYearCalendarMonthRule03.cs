﻿using System;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearCalendarMonthRule03 : IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IFcsCodeMappingHelper _mappingHelper;

        public CalendarYearCalendarMonthRule03(
            IReferenceDataService referenceDataService,
            IFcsCodeMappingHelper mappingHelper)
        {
            _referenceDataService = referenceDataService;
            _mappingHelper = mappingHelper;
        }

        public string ErrorMessage => "The CalendarMonth and CalendarYear is after the contract allocation end date.";

        public string ErrorName => "CalendarYearCalendarMonth_03";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var year = model.CalendarYear ?? 0;
            var month = model.CalendarMonth ?? 0;

            if (year == 0 || month == 0 || month < 1 || month > 12)
            {
                return false;
            }

            var startDateMonth = new DateTime(year, month, 1);

            var fcsDeliverableCode = _mappingHelper.GetFcsDeliverableCode(model, CancellationToken.None);
            var contractAllocation = _referenceDataService.GetContractAllocation(model.ConRefNumber, fcsDeliverableCode, CancellationToken.None);

            return contractAllocation != null && contractAllocation.EndDate > startDateMonth;
        }
    }
}