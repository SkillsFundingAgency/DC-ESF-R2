using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1819EF.Console.ValidEntities
{
    public partial class CollectionDetail
    {
        public int Ukprn { get; set; }
        public string Collection { get; set; }
        public string Year { get; set; }
        public DateTime? FilePreparationDate { get; set; }
    }
}
