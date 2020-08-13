using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Constants;

namespace ESFA.DC.ESF.R2.Data.FundingSummary
{
    public abstract class AbstractFundingSummaryYearConfiguration
    {
        public int BaseIlrYear => 2018;

        public Dictionary<int, string> BaseYearToAcademicYearDictionary() => new Dictionary<int, string>
        {
            { AcademicYearConstants.Year2018, AcademicYearConstants.CalendarYear1819 }
        };

        public Dictionary<int, string> BaseYearToCollectionDictionary() => new Dictionary<int, string>
        {
            { AcademicYearConstants.Year2018, AcademicYearConstants.CollectionILR1819 }
        };

        public IDictionary<int, string[]> PeriodisedValuesHeaderDictionary(int currentCollectionYear)
        {
            var collectionYears = new List<int>();
            var yearIteration = currentCollectionYear;

            while (yearIteration >= BaseIlrYear)
            {
                collectionYears.Add(yearIteration);
                yearIteration--;
            }

            return collectionYears.ToDictionary(x => x, x => BuildMonths(x));
        }

        private string[] BuildMonths(int year)
        {
            var startYearString = year.ToString();
            var endYearString = (year + 1).ToString();

            return new string[]
            {
                string.Concat("August ", startYearString),
                string.Concat("September ", startYearString),
                string.Concat("October ", startYearString),
                string.Concat("November ", startYearString),
                string.Concat("December ", startYearString),
                string.Concat("January ", endYearString),
                string.Concat("February ", endYearString),
                string.Concat("March ", endYearString),
                string.Concat("April ", endYearString),
                string.Concat("May ", endYearString),
                string.Concat("June ", endYearString),
                string.Concat("July ", endYearString)
            };
        }
    }
}
