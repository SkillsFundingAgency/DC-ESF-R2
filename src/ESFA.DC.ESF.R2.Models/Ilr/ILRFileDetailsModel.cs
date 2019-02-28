using System;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class ILRFileDetailsModel
    {
        public string FileName { get; set; }

        public DateTime? LastSubmission { get; set; }

        public int Year { get; set; }
    }
}