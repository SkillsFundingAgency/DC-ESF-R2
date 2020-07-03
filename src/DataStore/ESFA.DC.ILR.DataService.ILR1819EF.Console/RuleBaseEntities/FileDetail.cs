using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.RuleBaseEntities
{
    public partial class FileDetail
    {
        public long Id { get; set; }
        public int Ukprn { get; set; }
        public string Filename { get; set; }
        public long? FileSizeKb { get; set; }
        public int? TotalLearnersSubmitted { get; set; }
        public int? TotalValidLearnersSubmitted { get; set; }
        public int? TotalInvalidLearnersSubmitted { get; set; }
        public int? TotalErrorCount { get; set; }
        public int? TotalWarningCount { get; set; }
        public DateTime? SubmittedTime { get; set; }
        public bool? Success { get; set; }
    }
}
