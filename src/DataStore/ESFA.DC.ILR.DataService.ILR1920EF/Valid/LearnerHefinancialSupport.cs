namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class LearnerHefinancialSupport
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Fintype { get; set; }
        public int Finamount { get; set; }

        public virtual LearnerHe LearnerHe { get; set; }
    }
}
