﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class CG02CommunityGrantManagementCost : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public CG02CommunityGrantManagementCost(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "CG02";
    }
}