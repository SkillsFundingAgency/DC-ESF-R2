using System;

namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class TrailblazerApprenticeshipFinancialRecord
    {
        public int Id { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string TbfinType { get; set; }
        public int? TbfinCode { get; set; }
        public DateTime? TbfinDate { get; set; }
        public int TbfinAmount { get; set; }
    }
}
