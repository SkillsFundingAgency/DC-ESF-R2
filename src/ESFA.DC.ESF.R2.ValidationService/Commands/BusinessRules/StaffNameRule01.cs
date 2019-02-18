using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class StaffNameRule01 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The StaffName must be returned for the selected ReferenceType.";

        public string ErrorName => "StaffName_01";

        public bool IsWarning => false;

        public bool Execute(SupplementaryDataModel model)
        {
            return model.ReferenceType != "Employee ID" || !string.IsNullOrEmpty(model.StaffName?.Trim());
        }
    }
}
