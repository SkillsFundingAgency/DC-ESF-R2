﻿using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Constants
{
    public static class FundingSummaryReportConstants
    {
        // Learner Assessment Plan
        public const string Header_LearnerAssessment = "Learner Assessment and Plan";
        public const string Deliverable_ILR_ST01 = "ILR ST01 Learner Assessment and Plan (£)";
        public const string Deliverable_ESF_ST01 = "SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)";

        // Regulated Learning
        public const string Header_RegulatedLearning = "Regulated Learning";
        public const string Deliverable_ILR_RQ01_Start = "ILR RQ01 Regulated Learning - Start Funding (£)";
        public const string Deliverable_ILR_RQ01_Ach = "ILR RQ01 Regulated Learning - Achievement Funding (£)";
        public const string Deliverable_ESF_RQ01 = "SUPPDATA RQ01 Regulated Learning Authorised Claims (£)";

        // Non Regulated Learning
        public const string Header_NonRegulatedLearning = "Non Regulated Learning";
        public const string Deliverable_ILR_NR01_Start = "ILR NR01 Non Regulated Learning - Start Funding (£)";
        public const string Deliverable_ILR_NR01_Ach = "ILR NR01 Non Regulated Learning - Achievement Funding (£)";
        public const string Deliverable_ESF_NR01 = "SUPPDATA NR01 Non Regulated Learning Authorised Claims (£)";

        // Progression and Sustained Progression
        public const string Header_Progression = "Progression and Sustained Progression";
        public const string Deliverable_ILR_PG01 = "ILR PG01 Progression Paid Employment (£)";
        public const string Deliverable_ESF_PG01 = "SUPPDATA PG01 Progression Paid Employment Adjustments (£)";
        public const string Deliverable_ILR_PG03 = "ILR PG03 Progression Education (£)";
        public const string Deliverable_ESF_PG03 = "SUPPDATA PG03 Progression Education Adjustments (£)";
        public const string Deliverable_ILR_PG04 = "ILR PG04 Progression Apprenticeship (£)";
        public const string Deliverable_ESF_PG04 = "SUPPDATA PG04 Progression Apprenticeship Adjustments (£)";
        public const string Deliverable_ILR_PG05 = "ILR PG05 Progression Traineeship (£)";
        public const string Deliverable_ESF_PG05 = "SUPPDATA PG05 Progression Traineeship Adjustments (£)";

        // Community Grant
        public const string Header_CommunityGrant = "Community Grant";
        public const string Deliverable_ESF_CG01 = "SUPPDATA CG01 Community Grant Payment (£)";
        public const string Deliverable_ESF_CG02 = "SUPPDATA CG02 Community Grant Management Cost (£)";

        // Specification Defined
        public const string Header_SpecificationDefined = "Specification Defined";
        public const string Deliverable_ESF_SD01 = "SUPPDATA SD01 Progression Within Work (£)";
        public const string Deliverable_ESF_SD02 = "SUPPDATA SD02 LEP Agreed Delivery Plan (£)";

        // Attribute Lookups
        public const string IlrStartEarningsAttribute = "StartEarnings";
        public const string IlrAchievementEarningsAttribute = "AchievementEarnings";
        public const string IlrAAdditionalProgCostEarningsAttribute = "AdditionalProgCostEarnings";
        public const string IlrProgressionEarningsAttribute = "ProgressionEarnings";
        public const string EsfReferenceTypeAuthorisedClaims = "Authorised Claims";
    }
}
