using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IValidationErrorMessageCache
    {
        string GetErrorMessage(string ruleName);

        Task PopulateErrorMessages(CancellationToken cancellationToken);
    }
}