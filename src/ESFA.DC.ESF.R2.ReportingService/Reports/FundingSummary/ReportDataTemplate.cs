using System.Collections.Generic;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ESF.R2.ReportingService.Constants;

namespace ESFA.DC.ESF.R2.ReportingService.Reports.FundingSummary
{
    public static class ReportDataTemplate
    {
        public static readonly List<FundingReportRow> FundingModelRowDefinitions = new List<FundingReportRow>
        {
            new FundingReportRow { RowType = RowType.Spacer },
            new FundingReportRow { RowType = RowType.MainTitle, Title = "European Social Fund 2014-2020 (Round 2)" },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.Title, Title = "Learner Assessment and Plan" },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "ST01", RowType = RowType.Data, Title = "ILR ST01 Learner Assessment and Plan (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "ST01", RowType = RowType.Data, Title = "SUPPDATA ST01 Learner Assessment and Plan Adjustments (£)" },
            new FundingReportRow { DeliverableCode = "ST01", RowType = RowType.Total, Title = "Total Learner Assessment and Plan (£)" },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.Title, Title = "Regulated Learning" },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "RQ01", RowType = RowType.Data, Title = "ILR RQ01 Regulated Learning - Start Funding (£)", AttributeNames = new List<string> { "StartEarnings" } },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "RQ01", RowType = RowType.Data, Title = "ILR RQ01 Regulated Learning - Achievement Funding (£)", AttributeNames = new List<string> { "AchievementEarnings" } },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "RQ01", RowType = RowType.Total, Title = "ILR Total RQ01 Regulated Learning (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "RQ01", RowType = RowType.Data, Title = "SUPPDATA RQ01 Regulated Learning Authorised Claims (£)", CostType = "Authorised Claims" },
            new FundingReportRow { DeliverableCode = "RQ01", RowType = RowType.Total, Title = "Total Regulated Learning (£)" },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.Title, Title = "Non Regulated Activity" },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "NR01", RowType = RowType.Data, Title = "ILR NR01 Non Regulated Activity - Start Funding (£)", AttributeNames = new List<string> { "StartEarnings" } },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "NR01", RowType = RowType.Data, Title = "ILR NR01 Non Regulated Activity - Achievement Funding (£)", AttributeNames = new List<string> { "AchievementEarnings" } },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "NR01", RowType = RowType.Total, Title = "ILR Total NR01 Non Regulated Activity (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "NR01", RowType = RowType.Data, Title = "SUPPDATA NR01 Non Regulated Activity Authorised Claims (£)", CostType = "Authorised Claims" },
            new FundingReportRow { DeliverableCode = "NR01", RowType = RowType.Total, Title = "Total Non Regulated Activity (£)" },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.Title, Title = "Progression and Sustained Progression" },
            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "PG01", RowType = RowType.Data, Title = "ILR PG01 Progression Paid Employment (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "PG01", RowType = RowType.Data, Title = "SUPPDATA PG01 Progression Paid Employment Adjustments (£)" },
            new FundingReportRow { DeliverableCode = "PG01", RowType = RowType.Total, Title = "Total Paid Employment Progression (£)" },

            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "PG03", RowType = RowType.Data, Title = "ILR PG03 Progression Education (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "PG03", RowType = RowType.Data, Title = "SUPPDATA PG03 Progression Education Adjustments (£)" },
            new FundingReportRow { DeliverableCode = "PG03", RowType = RowType.Total, Title = "Total Education Progression (£)" },

            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "PG04", RowType = RowType.Data, Title = "ILR PG04 Progression Apprenticeship (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "PG04", RowType = RowType.Data, Title = "SUPPDATA PG04 Progression Apprenticeship Adjustments (£)" },
            new FundingReportRow { DeliverableCode = "PG04", RowType = RowType.Total, Title = "Total Apprenticeship Progression (£)" },

            new FundingReportRow { CodeBase = "ILR", DeliverableCode = "PG05", RowType = RowType.Data, Title = "ILR PG05 Progression Traineeship (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "PG05", RowType = RowType.Data, Title = "SUPPDATA PG05 Progression Traineeship Adjustments (£)" },
            new FundingReportRow { DeliverableCode = "PG05", RowType = RowType.Total, Title = "Total Traineeship Progression (£)" },

            new FundingReportRow
            {
                DeliverableCode = "PG01, PG03, PG04, PG05",
                RowType = RowType.Total,
                Title = "Total Progression and Sustained Progression (£)"
            },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.Title, Title = "Community Grant" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "CG01", RowType = RowType.Data, Title = "SUPPDATA CG01 Community Grant Payment (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "CG02", RowType = RowType.Data, Title = "SUPPDATA CG02 Community Grant Management Cost (£)" },
            new FundingReportRow { DeliverableCode = "CG01, CG02", RowType = RowType.Total, Title = "Total Community Grant (£)" },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.Title, Title = "Specification Defined" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "SD01", RowType = RowType.Data, Title = "SUPPDATA SD01 Progression Within Work (£)" },
            new FundingReportRow { CodeBase = "ESF", DeliverableCode = "SD02", RowType = RowType.Data, Title = "SUPPDATA SD02 LEP Agreed Delivery Plan (£)" },
            new FundingReportRow { DeliverableCode = "SD01, SD02, SD03, SD04, SD05, SD06, SD07, SD08, SD09, SD10", RowType = RowType.Total, Title = "Total Specification Defined (£)" },
            new FundingReportRow { RowType = RowType.Spacer },

            new FundingReportRow { RowType = RowType.FinalTotal, Title = ReportingConstants.ContractReferenceNumberTag + " Total (£)" },
            new FundingReportRow { RowType = RowType.FinalCumulative, Title = ReportingConstants.ContractReferenceNumberTag + " Cumulative (£)" },
            new FundingReportRow { RowType = RowType.Spacer }
        };
    }
}
