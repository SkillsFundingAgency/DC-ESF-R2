using System;

namespace ESFA.DC.ILR.DataService.ILR1718EF
{
    public partial class AecHistoricEarningOutput
    {
        public int Ukprn { get; set; }
        public string LearnRefNumber { get; set; }
        public string AppIdentifierOutput { get; set; }
        public bool? AppProgCompletedInTheYearOutput { get; set; }
        public int? HistoricDaysInYearOutput { get; set; }
        public DateTime? HistoricEffectiveTnpstartDateOutput { get; set; }
        public int? HistoricEmpIdEndWithinYearOutput { get; set; }
        public int? HistoricEmpIdStartWithinYearOutput { get; set; }
        public int? HistoricFworkCodeOutput { get; set; }
        public bool? HistoricLearner1618AtStartOutput { get; set; }
        public decimal? HistoricPmramountOutput { get; set; }
        public DateTime? HistoricProgrammeStartDateIgnorePathwayOutput { get; set; }
        public DateTime? HistoricProgrammeStartDateMatchPathwayOutput { get; set; }
        public int? HistoricProgTypeOutput { get; set; }
        public int? HistoricPwayCodeOutput { get; set; }
        public int? HistoricStdcodeOutput { get; set; }
        public decimal? HistoricTnp1output { get; set; }
        public decimal? HistoricTnp2output { get; set; }
        public decimal? HistoricTnp3output { get; set; }
        public decimal? HistoricTnp4output { get; set; }
        public decimal? HistoricTotal1618UpliftPaymentsInTheYear { get; set; }
        public decimal? HistoricTotalProgAimPaymentsInTheYear { get; set; }
        public long? HistoricUlnoutput { get; set; }
        public DateTime? HistoricUptoEndDateOutput { get; set; }
        public decimal? HistoricVirtualTnp3endofThisYearOutput { get; set; }
        public decimal? HistoricVirtualTnp4endofThisYearOutput { get; set; }
        public DateTime? HistoricLearnDelProgEarliestAct2dateOutput { get; set; }
    }
}
