using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Services
{
    public interface ILegacyILRService
    {
        Task<IEnumerable<ILRFileDetails>> GetPreviousYearsILRFileDetails(
            int ukPrn,
            CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValues>> GetPreviousYearsFM70Data(
            int ukPrn,
            CancellationToken cancellationToken);
    }
}