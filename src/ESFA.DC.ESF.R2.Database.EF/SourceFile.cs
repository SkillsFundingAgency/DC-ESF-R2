using System;
using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.Database.EF
{
    public partial class SourceFile
    {
        public SourceFile()
        {
            SupplementaryDatas = new HashSet<SupplementaryData>();
        }

        public int SourceFileId { get; set; }
        public string FileName { get; set; }
        public DateTime FilePreparationDate { get; set; }
        public string ConRefNumber { get; set; }
        public string Ukprn { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual ICollection<SupplementaryData> SupplementaryDatas { get; set; }
    }
}
