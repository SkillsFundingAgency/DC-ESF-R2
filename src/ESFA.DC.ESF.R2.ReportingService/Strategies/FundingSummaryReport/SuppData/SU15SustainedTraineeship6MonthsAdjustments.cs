﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class SU15SustainedTraineeship6MonthsAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public SU15SustainedTraineeship6MonthsAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "SU15";
    }
}