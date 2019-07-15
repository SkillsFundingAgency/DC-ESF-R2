using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.Services
{
    public interface IILRService
    {
        Task<IEnumerable<ILRFileDetails>> GetIlrFileDetails(int ukPrn, CancellationToken cancellationToken);

        Task<IEnumerable<FM70PeriodisedValuesYearly>> GetYearlyIlrData(
            int endYear,
            int ukPrn,
            CancellationToken cancellationToken);
    }
}