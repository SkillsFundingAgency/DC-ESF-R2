using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearningDelivery
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string LearnAimRef { get; set; }
        public int AimType { get; set; }
        public int AimSeqNumber { get; set; }
        public DateTime LearnStartDate { get; set; }
        public DateTime? OrigLearnStartDate { get; set; }
        public DateTime LearnPlanEndDate { get; set; }
        public int FundModel { get; set; }
        public int? ProgType { get; set; }
        public int? FworkCode { get; set; }
        public int? PwayCode { get; set; }
        public int? PartnerUkprn { get; set; }
        public string DelLocPostCode { get; set; }
        public int? AddHours { get; set; }
        public int? PriorLearnFundAdj { get; set; }
        public int? OtherFundAdj { get; set; }
        public string ConRefNumber { get; set; }
        public int? EmpOutcome { get; set; }
        public int? CompStatus { get; set; }
        public DateTime? LearnActEndDate { get; set; }
        public int? WithdrawReason { get; set; }
        public int? Outcome { get; set; }
        public DateTime? AchDate { get; set; }
        public string OutGrade { get; set; }
        public string SwsupAimId { get; set; }
        public string LrnDelFamAdl { get; set; }
        public string LrnDelFamAsl { get; set; }
        public string LrnDelFamEef { get; set; }
        public string LrnDelFamFfi { get; set; }
        public string LrnDelFamFln { get; set; }
        public string LrnDelFamHem1 { get; set; }
        public string LrnDelFamHem2 { get; set; }
        public string LrnDelFamHem3 { get; set; }
        public string LrnDelFamHhs1 { get; set; }
        public string LrnDelFamHhs2 { get; set; }
        public string LrnDelFamLdm1 { get; set; }
        public string LrnDelFamLdm2 { get; set; }
        public string LrnDelFamLdm3 { get; set; }
        public string LrnDelFamLdm4 { get; set; }
        public string LrnDelFamNsa { get; set; }
        public string LrnDelFamPod { get; set; }
        public string LrnDelFamRes { get; set; }
        public string LrnDelFamSof { get; set; }
        public string LrnDelFamSpp { get; set; }
        public string LrnDelFamTbs { get; set; }
        public string LrnDelFamWpl { get; set; }
        public string LrnDelFamWpp { get; set; }
        public string ProvSpecMonA { get; set; }
        public string ProvSpecMonB { get; set; }
        public string ProvSpecMonC { get; set; }
        public string ProvSpecMonD { get; set; }
    }
}
