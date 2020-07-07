using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;

namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR
{
    public interface IIlrFileData
    {
        CollectionYear CollectionYear { get; }

        IIlrFile IlrFile { get; }

        IEnumerable<IPeriodisedValue> PeriodisedValues { get; }
    }
}
