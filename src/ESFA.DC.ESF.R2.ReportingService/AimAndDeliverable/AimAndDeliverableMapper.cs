using CsvHelper.Configuration;
using ESFA.DC.ESF.R2.Interfaces;
using ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable.Model;

namespace ESFA.DC.ESF.R2.ReportingService.AimAndDeliverable
{
    public class AimAndDeliverableMapper : ClassMap<AimAndDeliverableReportRow>, IClassMapper
    {
        public AimAndDeliverableMapper()
        {
            int i = 0;
            Map(m => m.LearningDelivery.LearnRefNumber).Index(i++).Name("Learner reference number");
            Map(m => m.LearningDelivery.ULN).Index(i++).Name("Unique learner number");
            Map(m => m.LearningDelivery.FamilyName).Index(i++).Name("Family name");
            Map(m => m.LearningDelivery.GivenNames).Index(i++).Name("Given names");
            Map(m => m.LearningDelivery.AimSeqNumber).Index(i++).Name("Aim sequence number");
            Map(m => m.LearningDelivery.ConRefNumber).Index(i++).Name("Contract reference number");
            Map(m => m.LearningDelivery.DeliverableCode).Index(i++).Name("Deliverable code");
            Map(m => m.DeliverableName).Index(i++).Name("Deliverable name");
            Map(m => m.LearningDelivery.LearnAimRef).Index(i++).Name("Learning aim reference");
            Map(m => m.DeliverablePeriod.DeliverableUnitCost).Index(i++).Name("Unit cost(£)");
            Map(m => m.LearningDelivery.ApplicWeightFundRate).Index(i++).Name("Funding rate - LARS plus ESOL hours(£)");
            Map(m => m.LearningDelivery.AimValue).Index(i++).Name("Aim value(£)");
            Map(m => m.LarsLearningDelivery.LearnAimRefTitle).Index(i++).Name("Learning aim title");
            Map(m => m.LearningDelivery.PMUKPRN).Index(i++).Name("Pre - merger UKPRN");
            Map(m => m.LearningDelivery.CampId).Index(i++).Name("Campus identifier");
            Map(m => m.LearningDelivery.ProviderSpecLearnerMonitoring_A).Index(i++).Name("Provider specified learner monitoring(A)");
            Map(m => m.LearningDelivery.ProviderSpecLearnerMonitoring_B).Index(i++).Name("Provider specified learner monitoring(B)");
            Map(m => m.LearningDelivery.SWSupAimId).Index(i++).Name("Software supplier aim identifier");
            Map(m => m.LarsLearningDelivery.NotionalNVQLevelV2).Index(i++).Name("Notional NVQ level");
            Map(m => m.LarsLearningDelivery.SectorSubjectAreaTier2).Index(i++).Name("Tier 2 sector subject area");
            Map(m => m.LearningDelivery.AdjustedAreaCostFactor).Index(i++).Name("Area uplift");
            Map(m => m.LearningDelivery.AdjustedPremiumFactor).Index(i++).Name("Learning rate premium");
            Map(m => m.LearningDelivery.LearnStartDate).Index(i++).Name("Learning start date");
            Map(m => m.LearningDelivery.LDESFEngagementStartDate).Index(i++).Name("Learning start date of first assessment");
            Map(m => m.LearningDelivery.LearnPlanEndDate).Index(i++).Name("Learning planned end date");
            Map(m => m.LearningDelivery.CompStatus).Index(i++).Name("Completion status");
            Map(m => m.LearningDelivery.LearnActEndDate).Index(i++).Name("Learning actual end date");
            Map(m => m.LearningDelivery.Outcome).Index(i++).Name("Outcome");
            Map(m => m.LearningDelivery.AddHours).Index(i++).Name("Additional delivery hours");
            Map(m => m.LearningDelivery.LearningDeliveryFAM_RES).Index(i++).Name("Learning delivery funding and monitoring type - restart indicator");
            Map(m => m.LearningDelivery.ProviderSpecDeliveryMonitoring_A).Index(i++).Name("Provider specified delivery monitoring(A)");
            Map(m => m.LearningDelivery.ProviderSpecDeliveryMonitoring_B).Index(i++).Name("Provider specified delivery monitoring(B)");
            Map(m => m.LearningDelivery.ProviderSpecDeliveryMonitoring_C).Index(i++).Name("Provider specified delivery monitoring(C)");
            Map(m => m.LearningDelivery.ProviderSpecDeliveryMonitoring_D).Index(i++).Name("Provider specified delivery monitoring(D)");
            Map(m => m.LearningDelivery.PartnerUKPRN).Index(i++).Name("Sub contracted or partnership UKPRN");
            Map(m => m.LearningDelivery.DelLocPostCode).Index(i++).Name("Delivery location postcode");
            Map(m => m.LearningDelivery.LatestPossibleStartDate).TypeConverterOption.Format("dd/MM/yyyy").Index(i++).Name("Latest possible progression start date");
            Map(m => m.LearningDelivery.EligibleProgressionOutomeStartDate).TypeConverterOption.Format("dd/MM/yyyy").Index(i++).Name("Eligible outcome start date");
            Map(m => m.DPOutcome.OutEndDate).Index(i++).Name("Eligible outcome end date");
            Map(m => m.DPOutcome.OutCollDate).Index(i++).Name("Eligible outcome collection date");
            Map(m => m.ESFDPOutcome.OutDateForProgression).Index(i++).Name("Eligible outcome date used for progression length");
            Map(m => m.LearningDelivery.EligibleProgressionOutcomeType).Index(i++).Name("Eligible outcome type");
            Map(m => m.LearningDelivery.EligibleProgressionOutcomeCode).Index(i++).Name("Eligible outcome code");
            Map(m => m.ReportMonth).Index(i++).Name("Month");
            Map(m => m.DeliverablePeriod.DeliverableVolume).Index(i++).Name("Deliverable volume");
            Map(m => m.DeliverablePeriod.StartEarnings).Index(i++).Name("Start earnings(£)");
            Map(m => m.DeliverablePeriod.AchievementEarnings).Index(i++).Name("Achievement earnings(£)");
            Map(m => m.DeliverablePeriod.AdditionalProgCostEarnings).Index(i++).Name("Additional programme cost earnings(£)");
            Map(m => m.DeliverablePeriod.ProgressionEarnings).Index(i++).Name("Progression earnings(£)");
            Map(m => m.DeliverablePeriod.TotalEarnings).Index(i++).Name("Total earnings(£)");
            Map().Constant(string.Empty).Index(i).Name("OFFICIAL - SENSITIVE");
        }
    }
}