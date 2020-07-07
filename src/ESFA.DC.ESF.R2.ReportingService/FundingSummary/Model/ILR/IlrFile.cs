using System;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ILR
{
    public class IlrFile : IIlrFile
    {
        public string FileName { get; }

        public DateTime SubmittedDateTime { get; }

        public DateTime FilePrepDate { get; }
    }
}
