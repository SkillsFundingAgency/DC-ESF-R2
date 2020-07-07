using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Enum;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF;
using ESFA.DC.ESF.R2.Interfaces.Reports.FundingSummary.ESF;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ESF
{
    public class EsfFileData : IEsfFileData
    {
        public CollectionYear CollectionYear { get; set; }

        public IEsfFile EsfFile { get; set; }

        public IEnumerable<IEsfSuppData> EsfSuppData { get; set; }
    }
}
