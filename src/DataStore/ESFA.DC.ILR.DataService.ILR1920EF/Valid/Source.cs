using System;

namespace ESFA.DC.ILR.DataService.ILR1920EF.Valid
{
    public partial class Source
    {
        public string ProtectiveMarking { get; set; }
        public int Ukprn { get; set; }
        public string SoftwareSupplier { get; set; }
        public string SoftwarePackage { get; set; }
        public string Release { get; set; }
        public string SerialNo { get; set; }
        public DateTime? DateTime { get; set; }
        public string ReferenceData { get; set; }
        public string ComponentSetVersion { get; set; }
    }
}
