﻿using System;

namespace ESFA.DC.ESF.R2.Models
{
    public class SourceFileModel
    {
        public int SourceFileId { get; set; }

        public string ConRefNumber { get; set; }

        public string UKPRN { get; set; }

        public string FileName { get; set; }

        public DateTime? SuppliedDate { get; set; }

        public DateTime PreparationDate { get; set; }

        public long? JobId { get; set; }
    }
}
