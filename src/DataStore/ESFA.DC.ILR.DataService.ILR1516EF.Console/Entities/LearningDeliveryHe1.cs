using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearningDeliveryHe1
    {
        public int LearningDeliveryHeId { get; set; }
        public int LearningDeliveryId { get; set; }
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public long? AimSeqNumber { get; set; }
        public string Numhus { get; set; }
        public string Ssn { get; set; }
        public string Qualent3 { get; set; }
        public long? Soc2000 { get; set; }
        public long? Sec { get; set; }
        public string Ucasappid { get; set; }
        public long? Typeyr { get; set; }
        public long? Modestud { get; set; }
        public long? Fundlev { get; set; }
        public long? Fundcomp { get; set; }
        public double? Stuload { get; set; }
        public long? Yearstu { get; set; }
        public long? Mstufee { get; set; }
        public double? Pcolab { get; set; }
        public double? Pcfldcs { get; set; }
        public double? Pcsldcs { get; set; }
        public double? Pctldcs { get; set; }
        public long? Specfee { get; set; }
        public long? Netfee { get; set; }
        public long? Grossfee { get; set; }
        public string Domicile { get; set; }
        public long? Elq { get; set; }
        public string HepostCode { get; set; }
    }
}
