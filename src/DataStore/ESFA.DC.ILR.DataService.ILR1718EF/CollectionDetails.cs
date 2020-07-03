using System;

namespace ESFA.DC.ILR.DataService.ILR1718EF
{
    public partial class CollectionDetails
    {
        public int Id { get; set; }
        public int Ukprn { get; set; }
        public string Collection { get; set; }
        public string Year { get; set; }
        public DateTime? FilePreparationDate { get; set; }
    }
}
