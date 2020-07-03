using System;

namespace ESFA.DC.ILR.DataService.Models
{
    public class ILRFileDetails
    {
        public int Year { get; set; }

        public string FileName { get; set; }

        public DateTime? LastSubmission { get; set; }

        public DateTime? FilePreparationDate { get; set; }
    }
}