using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ReferenceData.FCS.Model;
using ESFA.DC.ReferenceData.LARS.Model;

namespace ESFA.DC.ESF.R2.ReportingService.Tests.Builders
{
    public class ReferenceDataBuilder
    {
        public static List<FcsDeliverableCodeMapping> BuildContractDeliverableCodeMapping()
        {
            return new List<FcsDeliverableCodeMapping>
            {
                new FcsDeliverableCodeMapping
                {
                    DeliverableName = "T01 Learner Assessment and Plan",
                    FcsDeliverableCode = "1",
                    FundingStreamPeriodCode = "ESF1420",
                    ExternalDeliverableCode = "ST01"
                },
                new FcsDeliverableCodeMapping
                {
                    DeliverableName = "Q01 Regulated Learning",
                    FcsDeliverableCode = "2",
                    FundingStreamPeriodCode = "ESF1420",
                    ExternalDeliverableCode = "RQ01"
                }
            };
        }

        public static IEnumerable<LarsLearningDeliveryModel> BuildLarsLearningDeliveries()
        {
            return new List<LarsLearningDeliveryModel>
            {
                new LarsLearningDeliveryModel
                {
                    LearnAimRef = "ZESF0001",
                    NotionalNVQLevelv2 = "X",
                    SectorSubjectAreaTier2 = -2.00M,
                    LearnAimRefTitle = "ESF learner start and assessment"
                }
            };
        }
    }
}
