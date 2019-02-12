using System.Threading;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface IFcsCodeMappingHelper
    {
        int GetFcsDeliverableCode(SupplementaryDataModel model, CancellationToken cancellationToken);
    }
}