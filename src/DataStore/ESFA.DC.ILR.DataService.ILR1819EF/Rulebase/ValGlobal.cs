﻿using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Rulebase
{
    public partial class ValGlobal
    {
        public ValGlobal()
        {
            ValLearners = new HashSet<ValLearner>();
        }

        public int Ukprn { get; set; }
        public string EmployerVersion { get; set; }
        public string Larsversion { get; set; }
        public string OrgVersion { get; set; }
        public string PostcodeVersion { get; set; }
        public string RulebaseVersion { get; set; }

        public virtual ICollection<ValLearner> ValLearners { get; set; }
    }
}
