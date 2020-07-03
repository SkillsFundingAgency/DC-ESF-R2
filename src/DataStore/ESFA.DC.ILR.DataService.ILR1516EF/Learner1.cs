using System;

namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class Learner1
    {
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string PrevLearnRefNumber { get; set; }
        public long? PrevUkprn { get; set; }
        public long? Uln { get; set; }
        public string FamilyName { get; set; }
        public string GivenNames { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public long? Ethnicity { get; set; }
        public string Sex { get; set; }
        public long? LlddhealthProb { get; set; }
        public string Ninumber { get; set; }
        public long? PriorAttain { get; set; }
        public long? Accom { get; set; }
        public long? Alscost { get; set; }
        public long? PlanLearnHours { get; set; }
        public long? PlanEephours { get; set; }
        public string MathGrade { get; set; }
        public string EngGrade { get; set; }
    }
}
