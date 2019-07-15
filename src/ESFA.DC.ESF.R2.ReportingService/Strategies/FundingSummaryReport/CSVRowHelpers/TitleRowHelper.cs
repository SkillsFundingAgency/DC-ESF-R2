using System.Collections.Generic;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers
{
    public class TitleRowHelper : IRowHelper
    {
        private readonly RowType RowType = RowType.Title;

        public bool IsMatch(RowType rowType)
        {
            return rowType == RowType;
        }

        public void Execute(
            int endYear,
            IList<FundingSummaryModel> reportOutput,
            FundingReportRow row,
            IEnumerable<SupplementaryDataYearlyModel> esfDataModels,
            IEnumerable<FM70PeriodisedValuesYearly> ilrData)
        {
            reportOutput.Add(new FundingSummaryModel(row.Title, HeaderType.All, 2));
        }
    }
}