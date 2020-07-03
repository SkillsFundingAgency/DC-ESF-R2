using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.ValidEntities
{
    public partial class LearnerDestinationandProgression
    {
        public LearnerDestinationandProgression()
        {
            Dpoutcomes = new HashSet<Dpoutcome>();
        }

        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long Uln { get; set; }

        public virtual ICollection<Dpoutcome> Dpoutcomes { get; set; }
    }
}
