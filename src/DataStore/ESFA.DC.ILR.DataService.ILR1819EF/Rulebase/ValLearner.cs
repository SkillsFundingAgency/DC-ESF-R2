namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class ValLearner
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }

        public virtual ValGlobal UkprnNavigation { get; set; }
    }
}
