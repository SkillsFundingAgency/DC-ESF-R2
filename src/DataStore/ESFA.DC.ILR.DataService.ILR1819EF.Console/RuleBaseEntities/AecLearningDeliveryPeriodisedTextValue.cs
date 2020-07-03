using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class AecLearningDeliveryPeriodisedTextValue
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string AttributeName { get; set; }
        public string Period1 { get; set; }
        public string Period2 { get; set; }
        public string Period3 { get; set; }
        public string Period4 { get; set; }
        public string Period5 { get; set; }
        public string Period6 { get; set; }
        public string Period7 { get; set; }
        public string Period8 { get; set; }
        public string Period9 { get; set; }
        public string Period10 { get; set; }
        public string Period11 { get; set; }
        public string Period12 { get; set; }

        public virtual AecLearningDelivery AecLearningDelivery { get; set; }
    }
}
