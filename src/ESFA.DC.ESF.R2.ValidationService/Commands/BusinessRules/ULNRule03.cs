using ESFA.DC.DateTimeProvider.Interface;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Helpers;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class ULNRule03 : IBusinessRuleValidator
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public ULNRule03(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public string ErrorMessage => "This ULN should not be used for months that are more than two months older than the current month.";

        public string ErrorName => "ULN_03";

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            return
                (model.ULN ?? 0) != 9999999999 ||
                MonthYearHelper.GetCalendarDateTime(model.CalendarYear, model.CalendarMonth) > _dateTimeProvider.GetNowUtc().AddMonths(-2);
        }
    }
}
