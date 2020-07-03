namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class LearnerFam
    {
        public int LearnerFamId { get; set; }
        public int LearnerId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string LearnFamtype { get; set; }
        public long? LearnFamcode { get; set; }
    }
}
