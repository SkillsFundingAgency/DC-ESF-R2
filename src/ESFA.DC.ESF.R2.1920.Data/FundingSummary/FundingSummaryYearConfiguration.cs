using System.Collections.Generic;
using ESFA.DC.ESF.R2.Data.FundingSummary;
using ESFA.DC.ESF.R2.Interfaces.Constants;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary;

namespace ESFA.DC.ESF.R2._1920.Data.FundingSummary
{
    public class FundingSummaryYearConfiguration : AbstractFundingSummaryYearConfiguration, IFundingSummaryYearConfiguration
    {
        public IDictionary<int, string> YearToAcademicYearDictionary()
        {
            var dictionary = BaseYearToAcademicYearDictionary;

            dictionary.Add(AcademicYearConstants.Year2019, AcademicYearConstants.CalendarYear1920);

            return dictionary;
        }

        public IDictionary<int, string> YearToCollectionDictionary()
        {
            var dictionary = BaseYearToCollectionDictionary;

            dictionary.Add(AcademicYearConstants.Year2019, AcademicYearConstants.CollectionILR1920);

            return dictionary;
        }
    }
}
