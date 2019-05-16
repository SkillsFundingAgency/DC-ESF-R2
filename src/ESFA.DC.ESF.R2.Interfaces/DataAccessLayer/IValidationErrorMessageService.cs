using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IValidationErrorMessageService
    {
        string GetErrorMessage(string ruleName);

        Task PopulateErrorMessages(CancellationToken cancellationToken);
    }
}