using System;

namespace ESFA.DC.ESF.R2.Interfaces.ReferenceData
{
    public interface IEsfSuppDataFile
    {
        string FileName { get; }

        DateTime SubmittedDateTime { get; }
    }
}
