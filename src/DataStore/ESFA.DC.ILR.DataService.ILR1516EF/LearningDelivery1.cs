using System;

namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class LearningDelivery1
    {
        public int LearningDeliveryId { get; set; }
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string LearnAimRef { get; set; }
        public long? AimType { get; set; }
        public long? AimSeqNumber { get; set; }
        public DateTime? LearnStartDate { get; set; }
        public DateTime? OrigLearnStartDate { get; set; }
        public DateTime? LearnPlanEndDate { get; set; }
        public long? FundModel { get; set; }
        public long? ProgType { get; set; }
        public long? FworkCode { get; set; }
        public long? PwayCode { get; set; }
        public long? PartnerUkprn { get; set; }
        public string DelLocPostCode { get; set; }
        public long? AddHours { get; set; }
        public long? PriorLearnFundAdj { get; set; }
        public long? OtherFundAdj { get; set; }
        public string ConRefNumber { get; set; }
        public long? EmpOutcome { get; set; }
        public long? CompStatus { get; set; }
        public DateTime? LearnActEndDate { get; set; }
        public long? WithdrawReason { get; set; }
        public long? Outcome { get; set; }
        public DateTime? AchDate { get; set; }
        public string OutGrade { get; set; }
        public string SwsupAimId { get; set; }
    }
}
