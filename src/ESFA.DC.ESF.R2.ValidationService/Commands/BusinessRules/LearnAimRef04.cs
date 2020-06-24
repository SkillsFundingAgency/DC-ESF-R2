using System;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Constants;
using ESFA.DC.ESF.R2.ValidationService.Helpers;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef04 : BaseValidationRule, IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;
        private readonly IMonthYearHelper _monthYearHelper;

        public LearnAimRef04(
            IValidationErrorMessageService errorMessageService,
            IReferenceDataService referenceDataService,
            IMonthYearHelper monthYearHelper)
            : base(errorMessageService)
        {
            _referenceDataService = referenceDataService;
            _monthYearHelper = monthYearHelper;
        }

        public override string ErrorName => RulenameConstants.LearnAimRef_04;

        public bool IsWarning => false;

        public bool IsValid(SupplementaryDataModel model)
        {
            if (string.IsNullOrEmpty(model.LearnAimRef?.Trim()))
            {
                return true;
            }

            var larsLearningDelivery = _referenceDataService.GetLarsLearningDelivery(model.LearnAimRef);

            if (larsLearningDelivery == null)
            {
                return true;
            }

            var monthYearDate = _monthYearHelper.GetFirstOfCalendarMonthDateTime(model.CalendarYear, model.CalendarMonth);

            return larsLearningDelivery.ValidityPeriods
                .Any(validityPeriod => (validityPeriod.ValidityStartDate ?? DateTime.MaxValue) <= monthYearDate &&
                                       (validityPeriod.ValidityEndDate == null || validityPeriod.ValidityEndDate >= monthYearDate));
        }
    }
}