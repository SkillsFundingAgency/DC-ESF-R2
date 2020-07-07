using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary.ESF;

namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF
{
    public interface IEsfFileData
    {
        CollectionYear CollectionYear { get; }

        IEsfFile EsfFile { get; }

        IEnumerable<IEsfSuppData> EsfSuppData { get; }
    }
}
