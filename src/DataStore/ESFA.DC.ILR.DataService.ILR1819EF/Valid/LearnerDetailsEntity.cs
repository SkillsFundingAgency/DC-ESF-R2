using System;
using System.ComponentModel.DataAnnotations;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Valid
{
    public class LearnerDetailsEntity
    {
        [Key]
        public int Id { get; set; }

        public int Ukprn { get; set; }

        public string ConRefNumber { get; set; }

        public string LearnAimRef { get; set; }

        public int? AimSeqNumber { get; set; }

        public DateTime? LearnStartDate { get; set; }

        public long? Uln { get; set; }

        public string LearnRefNumber { get; set; }

        public string CampId { get; set; }

        public int? Pmukprn { get; set; }

        public int? FundModel { get; set; }

        public DateTime? LearnPlanEndDate { get; set; }

        public DateTime? LearnActEndDate { get; set; }

        public int? CompStatus { get; set; }

        public string DelLocPostCode { get; set; }

        public int? Outcome { get; set; }

        public int? AddHours { get; set; }

        public int? PartnerUkprn { get; set; }

        public string SwsupAimId { get; set; }
    }
}