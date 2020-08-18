using System.Collections.Generic;

namespace ESFA.DC.ESF.R2.ReportingService.FundingSummary.Constants
{
    public static class FundingSummaryReportConstants
    {
        public const string BodyTitle = "European Social Fund 2014-2020 (round 2)";
        public const string Default_SubCateogryTitle = "Default Category should not render";

        public const string NotApplicable = "n/a";
        public const string DecimalFormat = "#,##0.00";
        public const string GrandTotalHeader = "Grand Total";

        // Learner Assessment Plan
        public const string Header_LearnerAssessment = "Learner Assessment and Plan";
        public const string Total_LearnerAssessment = "Total Learner Assessment and Plan (£)";
        public const string Deliverable_ILR_ST01 = "ILR ST01 Learner Assessment and Plan (£)";
        public const string Deliverable_ESF_ST01 = "SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)";

        // Regulated Learning
        public const string Header_RegulatedLearning = "Regulated Learning";
        public const string Total_RegulatedLearning = "Total Regulated Learning (£)";
        public const string SubCategoryHeader_IlrRegulatedLearning = "ILR Total RQ01 Regulated Learning (£)";
        public const string Deliverable_ILR_RQ01_Start = "ILR RQ01 Regulated Learning - Start Funding (£)";
        public const string Deliverable_ILR_RQ01_Ach = "ILR RQ01 Regulated Learning - Achievement Funding (£)";
        public const string Deliverable_ESF_RQ01 = "SUPPDATA RQ01 Regulated Learning Authorised Claims (£)";

        // Non Regulated Learning
        public const string Header_NonRegulatedActivity = "Non Regulated Activity";
        public const string Total_NonRegulatedActivity = "Total Non Regulated Activity (£)";
        public const string SubCategoryHeader_IlrNonRegulatedActivity = "ILR Total NR01 Non Regulated Activity (£)";
        public const string Deliverable_ILR_NR01_Start = "ILR NR01 Non Regulated Activity - Start Funding (£)";
        public const string Deliverable_ILR_NR01_Ach = "ILR NR01 Non Regulated Activity - Achievement Funding (£)";
        public const string Deliverable_ESF_NR01 = "SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)";

        // Progression and Sustained Progression
        public const string Header_Progression = "Progression and Sustained Progression";
        public const string Total_Progression = "Total Progression and Sustained Progression (£)";
        public const string SubCategoryHeader_PG01 = "Total Paid Employment Progression (£)";
        public const string SubCategoryHeader_PG03 = "Total Education Progression (£)";
        public const string SubCategoryHeader_PG04 = "Total Apprenticeship Progression (£)";
        public const string SubCategoryHeader_PG05 = "Total Traineeship Progression (£)";
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
        public const string Total_CommunityGrant = "Total Community Grant (£)";
        public const string Deliverable_ESF_CG01 = "SUPPDATA CG01 Community Grant Payment (£)";
        public const string Deliverable_ESF_CG02 = "SUPPDATA CG02 Community Grant Management Cost (£)";

        // Specification Defined
        public const string Header_SpecificationDefined = "Specification Defined";
        public const string Total_SpecificationDefined = "Total Specification Defined (£)";
        public const string Deliverable_ESF_SD01 = "SUPPDATA SD01 Progression Within Work (£)";
        public const string Deliverable_ESF_SD02 = "SUPPDATA SD02 LEP Agreed Delivery Plan (£)";

        // Attribute Lookups
        public const string IlrStartEarningsAttribute = "StartEarnings";
        public const string IlrAchievementEarningsAttribute = "AchievementEarnings";
        public const string IlrAAdditionalProgCostEarningsAttribute = "AdditionalProgCostEarnings";
        public const string IlrProgressionEarningsAttribute = "ProgressionEarnings";
        public const string EsfReferenceTypeAuthorisedClaims = "Authorised Claims";

        // Render constants
        public const string HeaderProviderName = "Provider Name : ";
        public const string HeaderUkprn = "UKPRN : ";
        public const string HeaderContractNumber = "Contract Reference Number : ";
        public const string HeaderEsfFileName = "Round 2 Supplementary Data File : ";
        public const string HeaderEsfFileUpdated = "Last Round 2 Supplementary Data File Update : ";
        public const string HeaderClassification = "Security Classification : ";

        public const string HeaderIlrFileName = "ILR File : ";
        public const string HeaderIlrFileUpdated = "Last ILR File Update : ";
        public const string HeaderIlrFilePrepDate = "File Preparation Date : ";

        public const string FooterApplicationVersion = "Application Version : ";
        public const string FooterLARSData = "LARS Data : ";
        public const string FooterPostcodeData = "Postcode Data : ";
        public const string FooterOrgData = "Organisation Data : ";
        public const string FooterReportGenerated = "Report generated at : ";
    }
}
