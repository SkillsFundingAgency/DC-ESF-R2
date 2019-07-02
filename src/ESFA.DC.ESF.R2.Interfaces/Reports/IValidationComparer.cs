using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports
{
    public interface IValidationComparer
    {
        int Compare(ValidationErrorModel first, ValidationErrorModel second);
    }
}