using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class TrailblazerApprenticeshipFinancialRecord1
    {
        public int TrailblazerApprenticeshipFinancialRecordId { get; set; }
        public int LearningDeliveryId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? AimSeqNumber { get; set; }
        public string TbfinType { get; set; }
        public long? TbfinCode { get; set; }
        public DateTime? TbfinDate { get; set; }
        public long? TbfinAmount { get; set; }
    }
}
