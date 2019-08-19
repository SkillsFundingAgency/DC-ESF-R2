using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;
using ESFA.DC.ILR.DataService.Models;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers
{
    public class CumulativeRowHelper : BaseDataRowHelper, IRowHelper
    {
        public bool IsMatch(RowType rowType)
        {
            return rowType == RowType.Cumulative || rowType == RowType.FinalCumulative;
        }

        public void Execute(
            int endYear,
            IList<FundingSummaryModel> reportOutput,
            FundingReportRow row,
            IEnumerable<SupplementaryDataYearlyModel> esfDataModels,
            IEnumerable<FM70PeriodisedValuesYearly> ilrData)
        {
            FundingSummaryModel rowModel = new FundingSummaryModel(row.Title, HeaderType.None, 3);
            FundingSummaryModel grandTotalRow = reportOutput.FirstOrDefault(r => r.Title == "<ESF-1> Total (£)");

            if (row.RowType == RowType.FinalCumulative)
            {
                rowModel.ExcelHeaderStyle = 6;
                rowModel.ExcelRecordStyle = 6;
            }

            if (grandTotalRow == null)
            {
                reportOutput.Add(rowModel);
                return;
            }

            var yearlyValues = InitialiseFundingYears(endYear, esfDataModels);
            var cumulativeTotal = 0M;
            foreach (var year in grandTotalRow.YearlyValues)
            {
                var yearValues = yearlyValues.FirstOrDefault(yv => yv.FundingYear == year.FundingYear);
                if (yearValues == null)
                {
                    continue;
                }

                for (var i = 0; i < year.Values.Count; i++)
                {
                    cumulativeTotal += year.Values[i];
                    yearValues.Values.Add(cumulativeTotal);
                }
            }

            decimal? yearEndCumulative = 0M;
            for (var index = 0; index < grandTotalRow.Totals.Count - 1; index++)
            {
                var total = grandTotalRow.Totals[index];
                yearEndCumulative += total ?? 0M;
                rowModel.Totals.Add(yearEndCumulative);
            }

            rowModel.Totals.Add(yearEndCumulative);

            rowModel.YearlyValues = yearlyValues;

            reportOutput.Add(rowModel);
        }
    }
}
