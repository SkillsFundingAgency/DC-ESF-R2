using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR
{
    public interface IIlrFileData
    {
        CollectionYear CollectionYear { get; }

        IIlrFile IlrFile { get; }

        IEnumerable<FM70PeriodisedValuesYearly> PeriodisedValues { get; }
    }
}
