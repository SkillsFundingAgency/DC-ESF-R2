using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.ValidEntities
{
    public partial class Learner
    {
        public Learner()
        {
            ContactPreferences = new HashSet<ContactPreference>();
            LearnerEmploymentStatuses = new HashSet<LearnerEmploymentStatus>();
            LearnerFams = new HashSet<LearnerFam>();
            LearningDeliveries = new HashSet<LearningDelivery>();
            LlddandHealthProblems = new HashSet<LlddandHealthProblem>();
            ProviderSpecLearnerMonitorings = new HashSet<ProviderSpecLearnerMonitoring>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string PrevLearnRefNumber { get; set; }
        public int? PrevUkprn { get; set; }
        public int? Pmukprn { get; set; }
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
        public string PostcodePrior { get; set; }
        public string Postcode { get; set; }
        public string AddLine1 { get; set; }
        public string AddLine2 { get; set; }
        public string AddLine3 { get; set; }
        public string AddLine4 { get; set; }
        public string TelNo { get; set; }
        public string Email { get; set; }
        public string CampId { get; set; }

        public virtual LearnerHe LearnerHe { get; set; }
        public virtual ICollection<ContactPreference> ContactPreferences { get; set; }
        public virtual ICollection<LearnerEmploymentStatus> LearnerEmploymentStatuses { get; set; }
        public virtual ICollection<LearnerFam> LearnerFams { get; set; }
        public virtual ICollection<LearningDelivery> LearningDeliveries { get; set; }
        public virtual ICollection<LlddandHealthProblem> LlddandHealthProblems { get; set; }
        public virtual ICollection<ProviderSpecLearnerMonitoring> ProviderSpecLearnerMonitorings { get; set; }
    }
}
