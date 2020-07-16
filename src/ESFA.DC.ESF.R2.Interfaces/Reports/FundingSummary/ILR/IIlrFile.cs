using System;

namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary.ILR
{
    public interface IIlrFile
    {
        string FileName { get; }

        DateTime? SubmittedDateTime { get; }

        DateTime? FilePrepDate { get; }
    }
}
