namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class ProcessingData
    {
        public long Id { get; set; }
        public int Ukprn { get; set; }
        public long FileDetailsId { get; set; }
        public string ProcessingStep { get; set; }
        public string ExecutionTime { get; set; }
    }
}
