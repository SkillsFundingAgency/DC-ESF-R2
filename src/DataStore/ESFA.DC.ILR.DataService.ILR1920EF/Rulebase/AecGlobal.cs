using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Rulebase
{
    public partial class AecGlobal
    {
        public AecGlobal()
        {
            AecLearners = new HashSet<AecLearner>();
        }

        public int Ukprn { get; set; }
        public string Larsversion { get; set; }
        public string RulebaseVersion { get; set; }
        public string Year { get; set; }

        public virtual ICollection<AecLearner> AecLearners { get; set; }
    }
}
