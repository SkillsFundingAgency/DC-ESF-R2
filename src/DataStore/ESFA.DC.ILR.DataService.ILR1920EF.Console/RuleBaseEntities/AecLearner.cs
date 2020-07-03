using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Console.RuleBaseEntities
{
    public partial class AecLearner
    {
        public AecLearner()
        {
            AecApprenticeshipPriceEpisodes = new HashSet<AecApprenticeshipPriceEpisode>();
            AecLearningDeliveries = new HashSet<AecLearningDelivery>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long Uln { get; set; }

        public virtual AecGlobal UkprnNavigation { get; set; }
        public virtual ICollection<AecApprenticeshipPriceEpisode> AecApprenticeshipPriceEpisodes { get; set; }
        public virtual ICollection<AecLearningDelivery> AecLearningDeliveries { get; set; }
    }
}
