using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary
{
    public interface IFundingSummaryYearConfiguration
    {
        int BaseIlrYear { get; }

        IDictionary<int, string> YearToAcademicYearDictionary();

        IDictionary<int, string> YearToCollectionDictionary();

        IDictionary<int, string[]> PeriodisedValuesHeaderDictionary(int currentCollectionYear);
    }
}
