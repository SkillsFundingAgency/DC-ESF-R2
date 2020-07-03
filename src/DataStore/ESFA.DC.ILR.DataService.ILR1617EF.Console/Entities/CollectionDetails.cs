using System;

namespace ESFA.DC.ILR.DataService.ILR1617EF.Console.Entities
{
    public partial class CollectionDetails
    {
        public int Ukprn { get; set; }
        public string Collection { get; set; }
        public string Year { get; set; }
        public DateTime? FilePreparationDate { get; set; }
    }
}
