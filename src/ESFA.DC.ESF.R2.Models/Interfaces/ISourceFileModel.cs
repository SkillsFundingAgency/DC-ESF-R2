using System;

namespace ESFA.DC.ESF.R2.Models.Interfaces
{
    public interface ISourceFileModel
    {
        int SourceFileId { get; }

        string ConRefNumber { get; }

        string UKPRN { get; }

        string FileName { get; }

        DateTime? SuppliedDate { get; }

        DateTime PreparationDate { get; }

        long? JobId { get; }
    }
}