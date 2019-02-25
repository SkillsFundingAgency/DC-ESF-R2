using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr
{
    public class SU15SustainedTraineeship6Months : BaseILRDataStrategy, IILRDataStrategy
    {
        protected override string DeliverableCode => "SU15";

        protected override List<string> AttributeNames => new List<string>
        {
            "StartEarnings",
            "AchievementEarnings",
            "AdditionalProgCostEarnings",
            "ProgressionEarnings"
        };
    }
}