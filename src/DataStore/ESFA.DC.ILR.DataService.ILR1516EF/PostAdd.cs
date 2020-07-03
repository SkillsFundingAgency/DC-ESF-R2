namespace ESFA.DC.ILR.DataService.ILR1516EF
{
    public partial class PostAdd
    {
        public int PostAddId { get; set; }
        public int LearnerContactId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? ContType { get; set; }
        public long? LocType { get; set; }
        public string AddLine1 { get; set; }
        public string AddLine2 { get; set; }
        public string AddLine3 { get; set; }
        public string AddLine4 { get; set; }
    }
}
