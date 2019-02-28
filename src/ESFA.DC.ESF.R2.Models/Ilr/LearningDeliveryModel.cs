using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Models.Ilr
{
    public class LearningDeliveryModel
    {
        public string ConRefNum { get; set; }

        public string LearnRefNumber { get; set; }

        public string LearnAimRef { get; set; }

        public int? AimSequenceNumber { get; set; }

        public int FundModel { get; set; }

        public string SwSupAimId { get; set; }

        public DateTime LearnStartDate { get; set; }

        public DateTime LearnPlanEndDate { get; set; }

        public int? CompStatus { get; set; }

        public DateTime? LearnActEndDate { get; set; }

        public int? Outcome { get; set; }

        public int? AddHours { get; set; }

        public int? PartnerUkPrn { get; set; }

        public string DelLocPostCode { get; set; }

        public IEnumerable<LearningDeliveryFamModel> LearningDeliveryFams { get; set; }

        public IEnumerable<ProviderSpecDeliveryMonitoringModel> ProviderSpecDeliveryMonitorings { get; set; }
    }
}