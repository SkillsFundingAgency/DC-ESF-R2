using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr
{
    public class RQ01RegulatedLearningAchievementEarnings : BaseILRDataStrategy, IILRDataStrategy
    {
        protected override string DeliverableCode => "RQ01";

        protected override List<string> AttributeNames => new List<string>
        {
            "AchievementEarnings"
        };
    }
}