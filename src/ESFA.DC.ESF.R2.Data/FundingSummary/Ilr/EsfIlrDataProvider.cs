using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ESFA.DC.ESF.R2.Interfaces.Reports.Services;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Data.FundingSummary.Ilr
{
    public class EsfIlrDataProvider : IlrDataProvider
    {
        public EsfIlrDataProvider(IDictionary<int, Func<SqlConnection>> ilrSqlConnectionFunc, Func<SqlConnection> esfSqlConnectionFunc, IReturnPeriodLookup returnPeriodLookup) : base(ilrSqlConnectionFunc, esfSqlConnectionFunc, returnPeriodLookup)
        {
        }

        public override async Task<ICollection<FM70PeriodisedValues>> GetIlrPeriodisedValuesAsync(int ukprn, int currentYear, string returnPeriod,
            IDictionary<int, string> ilrYearsToCollectionDictionary, CancellationToken cancellationToken)
        {
            var taskList = new List<Task<IEnumerable<FM70PeriodisedValues>>>();
            var periodisedValues = new List<FM70PeriodisedValues>();

            foreach (var year in ilrYearsToCollectionDictionary)
            {
                taskList.Add(GetAcademicYearIlrData(ukprn, year.Key, year.Value, R14, cancellationToken));
            }

            var taskResults = await Task.WhenAll(taskList);

            periodisedValues.AddRange((taskResults?.SelectMany(x => x)) ?? Enumerable.Empty<FM70PeriodisedValues>());

            return periodisedValues;
        }
    }
}
