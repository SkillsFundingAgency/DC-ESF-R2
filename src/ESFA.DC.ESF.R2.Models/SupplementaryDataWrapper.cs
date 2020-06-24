using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models
{
    public class SupplementaryDataWrapper
    {
        public SupplementaryDataWrapper()
        {
            SupplementaryDataLooseModels = new List<SupplementaryDataLooseModel>();
            SupplementaryDataModels = new List<SupplementaryDataModel>();
            ValidErrorModels = new List<ValidationErrorModel>();
        }

        public ICollection<SupplementaryDataLooseModel> SupplementaryDataLooseModels { get; set; }

        public ICollection<SupplementaryDataModel> SupplementaryDataModels { get; set; }

        public ICollection<ValidationErrorModel> ValidErrorModels { get; set; }
    }
}
