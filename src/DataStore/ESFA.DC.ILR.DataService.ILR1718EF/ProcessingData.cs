namespace ESFA.DC.ILR.DataService.ILR1718EF
{
    public partial class ProcessingData
    {
        public int Id { get; set; }
        public int? Ukprn { get; set; }
        public int? FileDetailsId { get; set; }
        public string ProcessingStep { get; set; }
        public string ExecutionTime { get; set; }
    }
}
