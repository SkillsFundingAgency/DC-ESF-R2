using ESFA.DC.ESF.R2.Interfaces.Validation;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.ValidationService.Commands.BusinessRules
{
    public class StaffNameRule03 : IBusinessRuleValidator
    {
        public string ErrorMessage => "The StaffName is not required for the selected ReferenceType.";

        public string ErrorName => "StaffName_03";

        public bool IsWarning => true;

        public bool Execute(SupplementaryDataModel model)
        {
            return model.ReferenceType == "EmployeeID" || string.IsNullOrEmpty(model.StaffName?.Trim());
        }
    }
}
