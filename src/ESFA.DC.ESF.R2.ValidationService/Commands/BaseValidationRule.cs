using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;

namespace ESFA.DC.ESF.R2.ValidationService.Commands
{
    public abstract class BaseValidationRule
    {
        protected readonly IValidationErrorMessageService _errorMessageService;

        protected BaseValidationRule(IValidationErrorMessageService errorMessageService)
        {
            _errorMessageService = errorMessageService;
        }

        public abstract string ErrorName { get; }

        public string ErrorMessage => _errorMessageService.GetErrorMessage(ErrorName);
    }
}