using System;

namespace ESFA.DC.ESF.R2.Interfaces.ReferenceData
{
    public interface IIlrFile
    {
        string FileName { get; }

        DateTime SubmittedDateTime { get; }

        DateTime FilePrepDate { get; }
    }
}
