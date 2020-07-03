namespace ESFA.DC.ILR.DataService.ILR1920EF.Rulebase
{
    public partial class EsfvalValidationError
    {
        public int Ukprn { get; set; }
        public long AimSeqNumber { get; set; }
        public string ErrorString { get; set; }
        public string FieldValues { get; set; }
        public string LearnRefNumber { get; set; }
        public string RuleId { get; set; }
    }
}
