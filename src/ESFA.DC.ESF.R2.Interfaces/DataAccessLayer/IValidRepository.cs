using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.Ilr;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IValidRepository
    {
        Task<IEnumerable<LearnerModel>> GetValidLearnerData(
            int ukPrn,
            string conRefNum,
            CancellationToken cancellationToken);

        Task<IEnumerable<DpOutcomeModel>> GetDPOutcomes(int ukPrn, CancellationToken cancellationToken);
    }
}