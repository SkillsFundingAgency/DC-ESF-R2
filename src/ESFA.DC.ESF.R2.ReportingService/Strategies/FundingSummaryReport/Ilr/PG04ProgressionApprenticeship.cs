﻿using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr
{
    public class PG04ProgressionApprenticeship : BaseILRDataStrategy, IILRDataStrategy
    {
        protected override string DeliverableCode => "PG04";

        protected override List<string> AttributeNames => new List<string>
        {
            "StartEarnings",
            "AchievementEarnings",
            "AdditionalProgCostEarnings",
            "ProgressionEarnings"
        };
    }
}