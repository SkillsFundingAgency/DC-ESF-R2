using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ULNRule03 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMonthYearHelper _monthYearHelper;

        public ULNRule03(
            IValidationErrorMessageService errorMessageService,
            IDateTimeProvider dateTimeProvider,
            IMonthYearHelper monthYearHelper)
            : base(errorMessageService)
        {
            _dateTimeProvider = dateTimeProvider;
            _monthYearHelper = monthYearHelper;
        }

        public override string ErrorName => RulenameConstants.ULN_03;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            var now = _dateTimeProvider.ConvertUtcToUk(_dateTimeProvider.GetNowUtc());
            var twoMonthsAgo = _monthYearHelper.GetFirstOfCalendarMonthDateTime(now.Year, now.Month).AddMonths(-2);

            return
                (model.ULN ?? 0) != ValidationConstants.TemporaryUln ||
                _monthYearHelper.GetFirstOfCalendarMonthDateTime(model.CalendarYear, model.CalendarMonth) > twoMonthsAgo;
        }
    }
}
