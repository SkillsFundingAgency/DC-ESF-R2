using System;
using System.Threading;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class CalendarYearCalendarMonthRule02 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IFcsCodeMappingHelper _mappingHelper;

        public CalendarYearCalendarMonthRule02(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService,
            IFcsCodeMappingHelper mappingHelper)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
            _mappingHelper = mappingHelper;
        }

        public override string ErrorName => "CalendarYearCalendarMonth_02";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var year = model.CalendarYear ?? 0;
            var month = model.CalendarMonth ?? 0;

            if (year == 0 || month == 0 || month < 1 || month > 12)
            {
                return false;
            }

            var startDateMonthEnd = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            var fcsDeliverableCode = _mappingHelper.GetFcsDeliverableCode(model, CancellationToken.None);
            var contractAllocation = _referenceDataService.GetContractAllocation(model.ConRefNumber, fcsDeliverableCode, CancellationToken.None);

            return contractAllocation == null || contractAllocation.StartDate < startDateMonthEnd;
        }
    }
}
