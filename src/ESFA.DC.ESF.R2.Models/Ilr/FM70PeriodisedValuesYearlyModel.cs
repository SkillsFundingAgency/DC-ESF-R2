using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class FM70PeriodisedValuesYearlyModel
    {
        public int FundingYear { get; set; }

        public IList<FM70PeriodisedValuesModel> Fm70PeriodisedValues { get; set; }
    }
}
