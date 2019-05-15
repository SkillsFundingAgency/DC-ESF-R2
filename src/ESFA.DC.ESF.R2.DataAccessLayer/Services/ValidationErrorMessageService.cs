using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;

namespace ESFA.DC.ESF.R2.DataAccessLayer.Services
{
    public class ValidationErrorMessageService : IValidationErrorMessageService
    {
        private readonly IValidationErrorMessageCache _cache;

        public ValidationErrorMessageService(IValidationErrorMessageCache validationErrorMessageCache)
        {
            _cache = validationErrorMessageCache;
        }

        public string GetErrorMessage(string ruleName)
        {
            return _cache.GetErrorMessage(ruleName);
        }

        public async Task PopulateErrorMessages(CancellationToken cancellationToken)
        {
            await _cache.PopulateErrorMessages(cancellationToken);
        }
    }
}