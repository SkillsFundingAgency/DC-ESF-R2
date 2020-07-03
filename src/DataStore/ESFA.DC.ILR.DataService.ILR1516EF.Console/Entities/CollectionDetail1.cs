using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class CollectionDetail1
    {
        public int Ukprn { get; set; }
        public int CollectionDetailsId { get; set; }
        public string Collection { get; set; }
        public string Year { get; set; }
        public DateTime? FilePreparationDate { get; set; }
    }
}
