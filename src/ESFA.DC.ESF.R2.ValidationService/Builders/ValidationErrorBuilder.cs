using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Builders
{
    public class ValidationErrorBuilder
    {
        public static ValidationErrorModel BuildValidationErrorModel(SupplementaryDataModel model,  IBaseValidator validator)
        {
            return new ValidationErrorModel
            {
                RuleName = validator.ErrorName,
                ErrorMessage = validator.ErrorMessage,
                IsWarning = validator.IsWarning,
                ConRefNumber = model.ConRefNumber,
                DeliverableCode = model.DeliverableCode,
                CalendarYear = model.CalendarYear.ToString(),
                CalendarMonth = model.CalendarMonth.ToString(),
                CostType = model.CostType,
                StaffName = model.StaffName,
                ProviderSpecifiedReference = model.ProviderSpecifiedReference,
                ULN = model.ULN.ToString(),
                ReferenceType = model.ReferenceType,
                Reference = model.Reference,
                LearnAimRef = model.LearnAimRef,
                SupplementaryDataPanelDate = model.SupplementaryDataPanelDate.ToString(),
                Value = model.Value.ToString()
            };
        }

        public static ValidationErrorModel BuildValidationErrorModel(SupplementaryDataLooseModel model, IBaseValidator validator)
        {
            return new ValidationErrorModel
            {
                RuleName = validator.ErrorName,
                ErrorMessage = validator.ErrorMessage,
                IsWarning = validator.IsWarning,
                ConRefNumber = model.ConRefNumber,
                DeliverableCode = model.DeliverableCode,
                CalendarYear = model.CalendarYear,
                CalendarMonth = model.CalendarMonth,
                CostType = model.CostType,
                StaffName = model.StaffName,
                ProviderSpecifiedReference = model.ProviderSpecifiedReference,
                ULN = model.ULN,
                ReferenceType = model.ReferenceType,
                Reference = model.Reference,
                LearnAimRef = model.LearnAimRef,
                SupplementaryDataPanelDate = model.SupplementaryDataPanelDate,
                Value = model.Value
            };
        }
    }
}
