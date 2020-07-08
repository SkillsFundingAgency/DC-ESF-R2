using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ILR
{
    public class IlrFileData : IIlrFileData
    {
        public CollectionYear CollectionYear { get; set; }

        public IIlrFile IlrFile { get; set; }

        public IEnumerable<IPeriodisedValue> PeriodisedValues { get; set; }
    }
}
