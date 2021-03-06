﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class PG01ProgressionPaidEmploymentAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public PG01ProgressionPaidEmploymentAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "PG01";
    }
}