using System;

namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class Learner
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string PrevLearnRefNumber { get; set; }
        public int? PrevUkprn { get; set; }
        public long Uln { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Ethnicity { get; set; }
        public string Sex { get; set; }
        public int LlddhealthProb { get; set; }
        public string Ninumber { get; set; }
        public int? PriorAttain { get; set; }
        public int? Accom { get; set; }
        public int? Alscost { get; set; }
        public int? PlanLearnHours { get; set; }
        public int? PlanEephours { get; set; }
        public string MathGrade { get; set; }
        public string EngGrade { get; set; }
        public string HomePostcode { get; set; }
        public string CurrentPostcode { get; set; }
        public int? LrnFamDla { get; set; }
        public int? LrnFamEcf { get; set; }
        public int? LrnFamEdf1 { get; set; }
        public int? LrnFamEdf2 { get; set; }
        public int? LrnFamEhc { get; set; }
        public int? LrnFamFme { get; set; }
        public int? LrnFamHns { get; set; }
        public int? LrnFamLda { get; set; }
        public int? LrnFamLsr1 { get; set; }
        public int? LrnFamLsr2 { get; set; }
        public int? LrnFamLsr3 { get; set; }
        public int? LrnFamLsr4 { get; set; }
        public int? LrnFamMcf { get; set; }
        public int? LrnFamNlm1 { get; set; }
        public int? LrnFamNlm2 { get; set; }
        public int? LrnFamPpe1 { get; set; }
        public int? LrnFamPpe2 { get; set; }
        public int? LrnFamSen { get; set; }
        public string ProvSpecMonA { get; set; }
        public string ProvSpecMonB { get; set; }
    }
}
