using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Models.Ilr;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Services
{
    public interface IILRService
    {
        Task<IEnumerable<ILRFileDetailsModel>> GetIlrFileDetails(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValuesYearlyModel>> GetYearlyIlrData(int ukPrn, CancellationToken cancellationToken);
    }
}