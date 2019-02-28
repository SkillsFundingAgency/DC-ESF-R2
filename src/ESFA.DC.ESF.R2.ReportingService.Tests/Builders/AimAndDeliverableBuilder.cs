using System;
using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models.Reports;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.Builders
{
    public class AimAndDeliverableBuilder
    {
        public static IEnumerable<AimAndDeliverableModel> BuildAimAndDeliverableModels()
        {
            return new List<AimAndDeliverableModel>
            {
                new AimAndDeliverableModel
                {
                    LearnRefNumber = "9900000004",
                    ULN = 9900000004,
                    ConRefNumber = "ESF-5000",
                    LearnAimRef = "ZESF0001",
                    DeliverableCode = "12345678",
                    AimSeqNumber = 1,
                    LearnStartDate = new DateTime(2016, 10, 19),
                    LearnPlanEndDate = new DateTime(2016, 10, 19),
                    PartnerUKPRN = 10048217,
                    DelLocPostCode = "BH12 4AR",
                    CompStatus = 2,
                    LearnActEndDate = new DateTime(2016, 10, 19),
                    Outcome = 1,
                    SWSupAimId = "00F40788-2618-46AC-A1BB-C9786CD66BDA",
                    LearnDelFAMCode = "1",
                    AdjustedAreaCostFactor = 1.0M,
                    AdjustedPremiumFactor = 1.0M,
                    AimValue = 400.0M,
                    ApplicWeightFundRate = 0.0M,
                    EligibleProgressionOutcomeCode = null,
                    EligibleProgressionOutcomeType = null,
                    EligibleProgressionOutomeStartDate = null,
                    LatestPossibleStartDate = null,
                    LDESFEngagementStartDate = new DateTime(2017, 10, 14, 0, 0, 0)
                }
            };
        }
    }
}