using System;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ESF
{
    public class EsfFile : IEsfFile
    {
        public string FileName { get; }

        public DateTime SubmittedDateTime { get; }
    }
}
