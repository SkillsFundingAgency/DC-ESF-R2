using System;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.ValidationService.Helpers;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class LearnAimRef04 : IBusinessRuleValidator
    {
        private readonly IReferenceDataService _referenceDataService;

        public LearnAimRef04(IReferenceDataService referenceDataService)
        {
            _referenceDataService = referenceDataService;
        }

        public string ErrorName => "LearnAimRef_04";

        public bool IsWarning => false;

        public string ErrorMessage => "The CalendarMonth/CalendarYear is not within the LARS funding validity dates for the LearnAimRef.";

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

            var monthYearDate = MonthYearHelper.GetCalendarDateTime(model.CalendarYear, model.CalendarMonth);

            return larsLearningDelivery.ValidityPeriods
                .Any(validityPeriod => (validityPeriod.ValidityStartDate ?? DateTime.MaxValue) <= monthYearDate &&
                                       (validityPeriod.ValidityEndDate == null || validityPeriod.ValidityEndDate >= monthYearDate));
        }
    }
}