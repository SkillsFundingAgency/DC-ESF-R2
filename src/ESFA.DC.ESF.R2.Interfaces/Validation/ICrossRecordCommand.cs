using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;

namespace ESFA.DC.ESF.R2.Interfaces.Validation
{
    public interface ICrossRecordCommand : IValidatorCommand
    {
        ICollection<SupplementaryDataModel> AllRecords { get; set; }
    }
}