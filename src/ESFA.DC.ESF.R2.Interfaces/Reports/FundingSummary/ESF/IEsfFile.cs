using System;

namespace ESFA.DC.ESF.R2.Interfaces.FundingSummary.ESF
{
    public interface IEsfFile
    {
        string FileName { get; }

        DateTime? SubmittedDateTime { get; }
    }
}
