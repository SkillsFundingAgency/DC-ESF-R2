using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ESF.R2.Interfaces.Strategies;
using ESFA.DC.ESF.R2.Models;
using ESFA.DC.ESF.R2.Models.Ilr;
using ESFA.DC.ESF.R2.Models.Reports;
using ESFA.DC.ESF.R2.Models.Reports.FundingSummaryReport;

namespace ESFA.DC.ESF.R2.ReportingService.Strategies.FundingSummaryReport.CSVRowHelpers
{
    public class TotalRowHelper : BaseDataRowHelper, IRowHelper
    {
        public bool IsMatch(RowType rowType)
        {
            return rowType == RowType.Total || rowType == RowType.FinalTotal;
        }

        public void Execute(
            IList<FundingSummaryModel> reportOutput,
            FundingReportRow row,
            IEnumerable<SupplementaryDataYearlyModel> esfDataModels,
            IEnumerable<FM70PeriodisedValuesYearlyModel> ilrData)
        {
            List<string> deliverableCodes = row.DeliverableCode?.Split(',').Select(x => x.Trim())
                .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            List<FundingSummaryModel> reportRowsToTotal = deliverableCodes == null ?
                reportOutput.Where(r => r.IsDataRow).ToList() :
                reportOutput.Where(r => deliverableCodes.Contains(r.DeliverableCode) && r.IsDataRow &&
                                       (string.IsNullOrEmpty(row.CodeBase) || r.CodeBase == row.CodeBase)).ToList();

            if (!reportRowsToTotal.Any())
            {
                return;
            }

            var rowModel = new FundingSummaryModel(row.Title, HeaderType.None, 3)
            {
                DeliverableCode = row.DeliverableCode,
                ExcelRecordStyle = 3,
                ExcelHeaderStyle = 3
            };

            if (string.IsNullOrEmpty(row.CodeBase))
            {
                rowModel.ExcelHeaderStyle = 2;
                rowModel.ExcelRecordStyle = 2;
            }

            if (row.RowType == RowType.FinalTotal)
            {
                rowModel.ExcelHeaderStyle = 6;
                rowModel.ExcelRecordStyle = 6;
            }

            var yearlyValueTotals = InitialiseFundingYears(esfDataModels);
            foreach (var year in reportRowsToTotal.First().YearlyValues)
            {
                var yearValues = yearlyValueTotals.FirstOrDefault(yv => yv.FundingYear == year.FundingYear);
                if (yearValues == null)
                {
                    continue;
                }

                List<FundingSummaryReportYearlyValueModel> periodValues = reportRowsToTotal.SelectMany(r => r.YearlyValues)
                    .Where(r => r.FundingYear == year.FundingYear).ToList();
                if (!periodValues.Any())
                {
                    continue;
                }

                var periodValueCount = periodValues.First().Values.Count;
                for (var i = 0; i < periodValueCount; i++)
                {
                    yearValues.Values.Add(periodValues.Sum(model => model.Values[i]));
                }
            }

            yearlyValueTotals.ForEach(v =>
            {
                rowModel.Totals.Add(v.Values.Sum());
            });

            rowModel.YearlyValues = yearlyValueTotals;

            rowModel.GrandTotal = rowModel.Totals.Sum() ?? 0M;

            reportOutput.Add(rowModel);
        }
    }
}