using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models
{
    public class JobContextModel
    {
        public long JobId { get; set; }

        public DateTime SubmissionDateTimeUtc { get; set; }

        public IReadOnlyList<string> Tasks { get; set; }

        public string FileName { get; set; }

        public int CurrentPeriod { get; set; }
    }
}