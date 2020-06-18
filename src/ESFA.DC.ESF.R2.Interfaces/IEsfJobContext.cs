using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Interfaces
{
    public interface IEsfJobContext
    {
        long JobId { get; }

        int UkPrn { get; }

        DateTime SubmissionDateTimeUtc { get; }

        IReadOnlyList<string> Tasks { get; }

        string BlobContainerName { get; }

        string FileName { get; }

        int CurrentPeriod { get; }

        string ReturnPeriod { get; }

        int CollectionYear { get; }

        string IlrReferenceDataKey { get; }

        string CollectionName { get; }
    }
}