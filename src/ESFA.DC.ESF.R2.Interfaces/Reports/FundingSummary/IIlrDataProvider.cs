using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary
{
    public interface IIlrDataProvider
    {
        Task<ICollection<ILRFileDetails>> GetIlrFileDetailsAsync(int ukprn, IEnumerable<int> ilrYears, CancellationToken cancellationToken);

        Task<ICollection<FM70PeriodisedValues>> GetIlrPeriodisedValuesAsync(int ukprn, int currentYear, string returnPeriod, IDictionary<int, string> ilrYearsToCollectionDictionary, CancellationToken cancellationToken);
    }
}
