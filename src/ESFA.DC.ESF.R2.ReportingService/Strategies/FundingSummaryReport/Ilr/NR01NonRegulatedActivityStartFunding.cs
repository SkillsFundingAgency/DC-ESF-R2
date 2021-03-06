﻿using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.Ilr
{
    public class NR01NonRegulatedActivityStartFunding : BaseILRDataStrategy, IILRDataStrategy
    {
        protected override string DeliverableCode => "NR01";

        protected override List<string> AttributeNames => new List<string>
        {
            "StartEarnings"
        };
    }
}