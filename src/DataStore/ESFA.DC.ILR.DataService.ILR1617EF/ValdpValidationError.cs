namespace ESFA.DC.ILR.DataService.ILR1617EF
{
    public partial class ValdpValidationError
    {
        public int Id { get; set; }
        public int Ukprn { get; set; }
        public string ErrorString { get; set; }
        public string FieldValues { get; set; }
        public string LearnRefNumber { get; set; }
        public string RuleId { get; set; }
    }
}
