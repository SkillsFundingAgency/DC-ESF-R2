using System.ComponentModel.DataAnnotations;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public class PeriodEndMetricsEntity
    {
        [Key]
        public int Id { get; set; }

        public string TransactionType { get; set; }

        public decimal EarningsYTD { get; set; }

        public decimal EarningsACT1{ get; set; }

        public decimal EarningsACT2{ get; set; }
    }
}