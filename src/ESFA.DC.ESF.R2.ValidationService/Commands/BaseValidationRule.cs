using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;

namespace ESFA.DC.ESF.R2.ValidationService.Commands
{
    public class BaseValidationRule
    {
        protected readonly IValidationErrorMessageService _errorMessageService;

        protected BaseValidationRule(IValidationErrorMessageService errorMessageService)
        {
            _errorMessageService = errorMessageService;
        }

        public virtual string ErrorName { get; set; }

        public string ErrorMessage => _errorMessageService.GetErrorMessage(ErrorName);
    }
}