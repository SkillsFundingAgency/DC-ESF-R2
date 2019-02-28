using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.Ilr;

namespace ESFA.DC.ESF.R2.Interfaces.DataAccessLayer
{
    public interface IFM70Repository
    {
        Task<ILRFileDetailsModel> GetFileDetails(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<Fm70LearningDeliveryModel>> GetLearningDeliveries(int ukPrn, CancellationToken cancellationToken);

        Task<IList<FM70PeriodisedValuesModel>> GetPeriodisedValues(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<Fm70DpOutcome>> GetOutcomes(int ukPrn, CancellationToken cancellationToken);
    }
}