using System;
using ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Model.ILR
{
    public class IlrFile : IIlrFile
    {
        public string FileName { get; set; }

        public DateTime SubmittedDateTime { get; set;  }

        public DateTime FilePrepDate { get; set; }
    }
}
