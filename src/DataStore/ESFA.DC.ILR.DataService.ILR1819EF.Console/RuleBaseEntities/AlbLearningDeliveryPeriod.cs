using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class AlbLearningDeliveryPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public int Period { get; set; }
        public decimal? AreaUpliftOnProgPayment { get; set; }
        public decimal? AreaUpliftBalPayment { get; set; }
        public int? Albcode { get; set; }
        public decimal? AlbsupportPayment { get; set; }
        public decimal? LearnDelCarLearnPilotOnProgPayment { get; set; }
        public decimal? LearnDelCarLearnPilotBalPayment { get; set; }

        public virtual AlbLearningDelivery AlbLearningDelivery { get; set; }
    }
}
