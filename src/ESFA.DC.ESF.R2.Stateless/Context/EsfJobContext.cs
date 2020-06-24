using System;
using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces;

namespace ESFA.DC.ESF.R2.Stateless.Context
{
    public class EsfJobContext : IEsfJobContext
    {
        public long JobId { get; set; }

        public int UkPrn { get; set; }

        public DateTime SubmissionDateTimeUtc { get; set; }

        public IReadOnlyList<string> Tasks { get; set; }

        public string BlobContainerName { get; set; }

        public string FileName { get; set; }

        public int CurrentPeriod { get; set; }

        public string ReturnPeriod => $"R{CurrentPeriod:D2}";

        public int CollectionYear { get; set; }

        public string IlrReferenceDataKey { get; set; }

        public string CollectionName { get; set; }
    }
}