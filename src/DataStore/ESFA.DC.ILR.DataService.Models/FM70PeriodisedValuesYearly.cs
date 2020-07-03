using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.Models
{
    public class FM70PeriodisedValuesYearly
    {
        public int FundingYear { get; set; }

        public IList<FM70PeriodisedValues> Fm70PeriodisedValues { get; set; }
    }
}
