namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class Fm25Fm35LearnerPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int Period { get; set; }
        public decimal? LnrOnProgPay { get; set; }

        public virtual Fm25Learner Fm25Learner { get; set; }
        public virtual Fm25Fm35Global UkprnNavigation { get; set; }
    }
}
