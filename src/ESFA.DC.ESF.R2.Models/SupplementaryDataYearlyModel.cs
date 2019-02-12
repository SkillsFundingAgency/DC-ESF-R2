using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models
{
    public class SupplementaryDataYearlyModel
    {
        public int FundingYear { get; set; }

        public IList<SupplementaryDataModel> SupplementaryData { get; set; }
    }
}