using System.Threading;
using System.Threading.Tasks;

namespace ESFA.DC.ESF.R2.Interfaces.Reports
{
    public interface IModelBuilder<T>
    {
        Task<T> Build(IEsfJobContext esfJobContext, CancellationToken cancellationToken);
    }
}
