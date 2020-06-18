using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.Controllers
{
    public interface IServiceController
    {
        Task RunTasks(
            IEsfJobContext esfJobContext,
            CancellationToken cancellationToken);
    }
}