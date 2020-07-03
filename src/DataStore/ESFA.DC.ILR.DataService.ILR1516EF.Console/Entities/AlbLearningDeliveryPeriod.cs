﻿using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class AlbLearningDeliveryPeriod
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public int Period { get; set; }
        public int? Albcode { get; set; }
        public decimal? AlbsupportPayment { get; set; }
        public decimal? AreaUpliftBalPayment { get; set; }
        public decimal? AreaUpliftOnProgPayment { get; set; }
    }
}
