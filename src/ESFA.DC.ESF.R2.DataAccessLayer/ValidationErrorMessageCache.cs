using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;

namespace ESFA.DC.ESF.R2.DataAccessLayer
{
    public class ValidationErrorMessageCache : IValidationErrorMessageCache
    {
        private readonly IEsfRepository _esfRepository;

        public ValidationErrorMessageCache(IEsfRepository esfRepository)
        {
            _esfRepository = esfRepository;

            ValidationErrorMessages = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        private IDictionary<string, string> ValidationErrorMessages { get; set; }

        public string GetErrorMessage(string ruleName)
        {
            ValidationErrorMessages.TryGetValue(ruleName, out var message);

            return message;
        }

        public async Task PopulateErrorMessages(CancellationToken cancellationToken)
        {
            ValidationErrorMessages = await _esfRepository.GetValidationErrorMessages(cancellationToken);
        }
    }
}