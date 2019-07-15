using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.Reports;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Services
{
    public interface IAimAndDeliverableService1819
    {
        Task<IEnumerable<AimAndDeliverableModel>> GetAimAndDeliverableModel(
            int ukPrn,
            CancellationToken cancellationToken);
    }
}