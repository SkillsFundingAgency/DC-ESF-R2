﻿using ESFA.DC.ESF.R2.Interfaces.DataAccessLayer;
using ESFA.DC.ESF.R2.Interfaces.Reports.Strategies;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.SuppData
{
    public class SU12SustainedUnpaidEmployment6MonthsAdjustments : BaseSupplementaryDataStrategy, ISupplementaryDataStrategy
    {
        public SU12SustainedUnpaidEmployment6MonthsAdjustments(IReferenceDataService referenceDataService)
            : base(referenceDataService)
        {
        }

        protected override string DeliverableCode => "SU12";
    }
}