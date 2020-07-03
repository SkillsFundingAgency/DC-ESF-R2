namespace ESFA.DC.ILR.DataService.ILR1920EF.Rulebase
{
    public partial class AlbLearnerPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Period { get; set; }
        public int? AlbseqNum { get; set; }

        public virtual AlbLearner AlbLearner { get; set; }
    }
}
