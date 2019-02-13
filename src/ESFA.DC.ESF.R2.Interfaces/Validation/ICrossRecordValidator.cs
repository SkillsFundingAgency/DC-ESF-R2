using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface ICrossRecordValidator : IBaseValidator
    {
        bool Execute(IList<SupplementaryDataModel> allRecords, SupplementaryDataModel model);
    }
}