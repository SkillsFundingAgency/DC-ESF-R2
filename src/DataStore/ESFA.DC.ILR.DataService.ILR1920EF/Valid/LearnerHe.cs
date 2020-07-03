using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class LearnerHe
    {
        public LearnerHe()
        {
            LearnerHefinancialSupports = new HashSet<LearnerHefinancialSupport>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string Ucasperid { get; set; }
        public int? Ttaccom { get; set; }

        public virtual Learner Learner { get; set; }
        public virtual ICollection<LearnerHefinancialSupport> LearnerHefinancialSupports { get; set; }
    }
}
