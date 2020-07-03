using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Rulebase
{
    public partial class TblGlobal
    {
        public TblGlobal()
        {
            TblLearners = new HashSet<TblLearner>();
        }

        public int Ukprn { get; set; }
        public string CurFundYr { get; set; }
        public string Larsversion { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<TblLearner> TblLearners { get; set; }
    }
}
