using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.DataService.ILR1516EF.Console.Entities
{
    public partial class LearningDeliveryHe
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public int AimSeqNumber { get; set; }
        public string Numhus { get; set; }
        public string Ssn { get; set; }
        public string Qualent3 { get; set; }
        public int? Soc2000 { get; set; }
        public int? Sec { get; set; }
        public string Ucasappid { get; set; }
        public int Typeyr { get; set; }
        public int Modestud { get; set; }
        public int Fundlev { get; set; }
        public int Fundcomp { get; set; }
        public decimal? Stuload { get; set; }
        public int Yearstu { get; set; }
        public int Mstufee { get; set; }
        public decimal? Pcolab { get; set; }
        public decimal? Pcfldcs { get; set; }
        public decimal? Pcsldcs { get; set; }
        public decimal? Pctldcs { get; set; }
        public int Specfee { get; set; }
        public int? Netfee { get; set; }
        public int? Grossfee { get; set; }
        public string Domicile { get; set; }
        public int? Elq { get; set; }
        public string HepostCode { get; set; }
    }
}
