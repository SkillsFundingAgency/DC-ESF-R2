﻿using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class TblLearner
    {
        public TblLearner()
        {
            TblLearningDeliveries = new HashSet<TblLearningDelivery>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }

        public virtual TblGlobal UkprnNavigation { get; set; }
        public virtual ICollection<TblLearningDelivery> TblLearningDeliveries { get; set; }
    }
}
